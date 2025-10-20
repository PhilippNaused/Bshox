using System.Runtime.CompilerServices;
using TUnit.Assertions.Conditions;
using TUnit.Assertions.Core;

namespace Bshox.Tests;

public static class SequenceEqualAssertions
{
    public static CollectionComparerBasedAssertion<TItem> IsSequenceEqualTo<TItem>(this IAssertionSource<IEnumerable<TItem>> source, IEnumerable<TItem> expected, [CallerArgumentExpression(nameof(expected))] string? expectedExpression = null)
    {
        source.Context.ExpressionBuilder.Append(".IsSequenceEqualTo("
            + string.Join(", ", new[] { expectedExpression }.Where(e => e != null)) + ")");
        return new SequenceEqualToAssertion<TItem>(source.Context, expected, null);
    }

    public static CollectionComparerBasedAssertion<TItem> IsSequenceEqualTo<TItem>(this IAssertionSource<IEnumerable<TItem>> source, IEnumerable<TItem> expected, IEqualityComparer<TItem> comparer, [CallerArgumentExpression(nameof(expected))] string? expectedExpression = null, [CallerArgumentExpression(nameof(comparer))] string? comparerExpression = null)
    {
        source.Context.ExpressionBuilder.Append(".IsSequenceEqualTo("
            + string.Join(", ", new[] { expectedExpression, comparerExpression }.Where(e => e != null)) + ")");
        return new SequenceEqualToAssertion<TItem>(source.Context, expected, comparer);
    }

    private sealed class SequenceEqualToAssertion<TItem> : CollectionComparerBasedAssertion<TItem>
    {
        private readonly IEnumerable<TItem> expected;

        public SequenceEqualToAssertion(AssertionContext<IEnumerable<TItem>> context,
            IEnumerable<TItem> expected,
            IEqualityComparer<TItem>? comparer) : base(context)
        {
            this.expected = expected;
            if (comparer != null)
                SetComparer(comparer);
        }

        private AssertionResult Check(EvaluationMetadata<IEnumerable<TItem>> metadata)
        {
            var actual = metadata.Value;
            var exception = metadata.Exception;

            if (exception != null)
            {
                return AssertionResult.Failed($"threw {exception.GetType().Name}");
            }

            if (actual == null)
            {
                return AssertionResult.Failed("collection was null");
            }

            var comparer = HasCustomComparer() ? GetComparer() : EqualityComparer<TItem>.Default;

            var actualList = actual.ToList();
            var expectedList = expected.ToList();

            if (actualList.Count != expectedList.Count)
            {
                return AssertionResult.Failed(
                    $"collection has {actualList.Count} items but expected {expectedList.Count}");
            }

            for (int i = 0; i < actualList.Count; i++)
            {
                var actualItem = actualList[i];
                var expectedItem = expectedList[i];

                bool areEqual = actualItem == null && expectedItem == null ||
                               actualItem != null && expectedItem != null && comparer.Equals(actualItem, expectedItem);

                if (!areEqual)
                {
                    return AssertionResult.Failed(
                        $"collection item at index {i} does not match: expected {expectedItem}, but was {actualItem}");
                }
            }

            return AssertionResult.Passed;
        }

        protected override Task<AssertionResult> CheckAsync(EvaluationMetadata<IEnumerable<TItem>> metadata)
        {
            return Task.FromResult(Check(metadata));
        }
    }
}
