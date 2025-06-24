namespace Bshox.Generator.Tests;

internal class SurrogatesTests
{
    [Test]
    public async Task DateTimeOffsetSerializer()
    {
        const string sourceCode = """
                                  using System;
                                  using System.ComponentModel;
                                  using Bshox.Attributes;

                                  namespace TestModels;

                                  [BshoxSerializer(typeof(DateTimeOffset), Surrogates = [typeof(DateTimeOffsetSurrogate)])]
                                  partial class DateTimeOffsetSerializer;

                                  [BshoxSurrogate<DateTimeOffset>]
                                  struct DateTimeOffsetSurrogate
                                  {
                                      public DateTimeOffsetSurrogate(DateTimeOffset value)
                                      {
                                          UtcTicks = value.UtcTicks;
                                  #if NET8_0_OR_GREATER
                                          TotalOffsetMinutes = (short)value.TotalOffsetMinutes;
                                  #else
                                          TotalOffsetMinutes = (short)value.Offset.TotalMinutes;
                                  #endif
                                      }

                                      [BshoxMember(1)]
                                      public long UtcTicks { get; set; }

                                      [BshoxMember(2)]
                                      [DefaultValue(0)]
                                      public short TotalOffsetMinutes { get; set; }

                                      public readonly DateTimeOffset Convert() => new DateTimeOffset(UtcTicks, TimeSpan.Zero).ToOffset(TimeSpan.FromMinutes(TotalOffsetMinutes));
                                  }
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).IsEmpty();
        await Utils.ValidateOutput(generatedOutput, 3);
    }

    [Test]
    public async Task SurrogateShouldHaveSuffix()
    {
        const string sourceCode = """
                                  using System;
                                  using System.ComponentModel;
                                  using Bshox.Attributes;

                                  namespace TestModels;

                                  [BshoxSerializer(typeof(DateTimeOffset), Surrogates = [typeof(DateTimeOffsetSubstitute)])]
                                  public partial class DateTimeOffsetSerializer;

                                  [BshoxSurrogate<DateTimeOffset>(ImplicitMembers = true)]
                                  public record struct DateTimeOffsetSubstitute
                                  {
                                      public DateTimeOffsetSubstitute(DateTimeOffset value)
                                      {
                                          DateTimeTicks = value.Ticks;
                                          OffsetMinutes = (short)value.Offset.TotalMinutes;
                                      }

                                      public long DateTimeTicks { get; set; }

                                      [DefaultValue(0)]
                                      public short OffsetMinutes { get; set; }

                                      public readonly DateTimeOffset Convert() => new(DateTimeTicks, TimeSpan.FromMinutes(OffsetMinutes));
                                  }
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).HasSingleItem();
        await diagnostics.Single().AssertEqual(Diagnostics.SurrogateShouldHaveSuffix, "The surrogate type 'DateTimeOffsetSubstitute' should have the 'Surrogate' suffix");
        await Assert.That(generatedOutput).IsNotEmpty(); // Just a warning, so we still generate the output
    }

    [Test]
    public async Task MissingAttribute()
    {
        const string sourceCode = """
                                  using System;
                                  using System.ComponentModel;
                                  using Bshox.Attributes;

                                  namespace TestModels;

                                  [BshoxSerializer(typeof(DateTimeOffset), Surrogates = [typeof(DateTimeOffsetSurrogate)])]
                                  public partial class DateTimeOffsetSerializer;

                                  // no attribute
                                  public record struct DateTimeOffsetSurrogate
                                  {
                                      public DateTimeOffsetSurrogate(DateTimeOffset value)
                                      {
                                          DateTimeTicks = value.Ticks;
                                          OffsetMinutes = (short)value.Offset.TotalMinutes;
                                      }

                                      public long DateTimeTicks { get; set; }

                                      [DefaultValue(0)]
                                      public short OffsetMinutes { get; set; }

                                      public readonly DateTimeOffset Convert() => new(DateTimeTicks, TimeSpan.FromMinutes(OffsetMinutes));
                                  }
                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).HasSingleItem();
        await diagnostics[0].AssertEqual(Diagnostics.SurrogateTypeMustHaveAttribute, "The surrogate type 'TestModels.DateTimeOffsetSurrogate' must have the [BshoxSurrogate<T>] attribute");
        await Assert.That(generatedOutput).IsEmpty();
    }

    [Test]
    [Arguments("")] // missing
    [Arguments("public static DateTimeOffset Convert() => DateTimeOffset.Now;")] // not instance method
    [Arguments("public DateTime Convert() => new(DateTimeTicks);")] // wrong return type
    [Arguments("public DateTimeOffset ConvertTo() => new(DateTimeTicks, TimeSpan.FromMinutes(OffsetMinutes));")] // wrong method name
    [Arguments("internal DateTimeOffset Convert() => new(DateTimeTicks, TimeSpan.FromMinutes(OffsetMinutes));")] // wrong access modifier
    public async Task SurrogateMustHaveCorrectConvertMethod(string method)
    {
        string sourceCode = $$"""
                              using System;
                              using System.ComponentModel;
                              using Bshox.Attributes;

                              namespace TestModels;

                              [BshoxSerializer(typeof(DateTimeOffset), Surrogates = [typeof(DateTimeOffsetSurrogate)])]
                              public partial class DateTimeOffsetSerializer;

                              [BshoxSurrogate<DateTimeOffset>(ImplicitMembers = true)]
                              public record struct DateTimeOffsetSurrogate
                              {
                                  public DateTimeOffsetSurrogate(DateTimeOffset value)
                                  {
                                      DateTimeTicks = value.Ticks;
                                      OffsetMinutes = (short)value.Offset.TotalMinutes;
                                  }

                                  public long DateTimeTicks { get; set; }

                                  [DefaultValue(0)]
                                  public short OffsetMinutes { get; set; }

                                  {{method}}
                              }
                              """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);
        await Assert.That(diagnostics).HasSingleItem();
        await diagnostics.Single().AssertEqual(Diagnostics.SurrogateMustHaveCorrectConvertMethod, "The surrogate type 'TestModels.DateTimeOffsetSurrogate' must have a public method with signature 'public System.DateTimeOffset Convert()'");
        await Assert.That(generatedOutput).IsEmpty();
    }

    [Test]
    [Arguments("")] // missing
    [Arguments("public DateTimeOffsetSurrogate(DateTimeOffset value, int extra) { }")] // extra parameter
    [Arguments("public DateTimeOffsetSurrogate() { }")] // missing parameter
    [Arguments("internal DateTimeOffsetSurrogate(DateTimeOffset value) { }")] // wrong access modifier
    [Arguments("public DateTimeOffsetSurrogate(DateTime value) { }")] // wrong parameter type
    public async Task SurrogateMustHaveCorrectConstructor(string constructor)
    {
        string sourceCode = $$"""
                              using System;
                              using System.ComponentModel;
                              using Bshox.Attributes;

                              namespace TestModels;

                              [BshoxSerializer(typeof(DateTimeOffset), Surrogates = [typeof(DateTimeOffsetSurrogate)])]
                              public partial class DateTimeOffsetSerializer;

                              [BshoxSurrogate<DateTimeOffset>(ImplicitMembers = true)]
                              public record struct DateTimeOffsetSurrogate
                              {
                                  {{constructor}}

                                  public long DateTimeTicks { get; set; }

                                  [DefaultValue(0)]
                                  public short OffsetMinutes { get; set; }

                                  public readonly DateTimeOffset Convert() => new(DateTimeTicks, TimeSpan.FromMinutes(OffsetMinutes));
                              }
                              """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);
        await Assert.That(diagnostics).HasSingleItem();
        await diagnostics.Single().AssertEqual(Diagnostics.SurrogateMustHaveCorrectConstructor, "The surrogate type 'TestModels.DateTimeOffsetSurrogate' must have a public constructor that takes a 'System.DateTimeOffset'");
        await Assert.That(generatedOutput).IsEmpty();
    }

    [Test]
    public async Task InvalidType()
    {
        const string sourceCode = """
                                  using Bshox.Attributes;

                                  namespace TestModels;

                                  [BshoxSerializer(typeof(int), Surrogates = [typeof(int[])])]
                                  public partial class Serializer1;

                                  """;
        var generatedOutput = Utils.GetGeneratedOutput(sourceCode, out var diagnostics);

        await Assert.That(diagnostics).HasSingleItem();
        var diagnostic = diagnostics.Single();
        await Assert.That(diagnostic.Id).IsEqualTo("BSHOX999");
        await Assert.That(generatedOutput).IsEmpty();
    }
}
