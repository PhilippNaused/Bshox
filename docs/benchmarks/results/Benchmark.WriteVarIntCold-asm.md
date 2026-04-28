## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: Dry(IterationCount=1, LaunchCount=25, RunStrategy=ColdStart, UnrollFactor=1, WarmupCount=1))

```asm
; Benchmark.WriteVarInt.WriteByte()
       push      rbp
       sub       rsp,90
       lea       rbp,[rsp+90]
       xor       eax,eax
       mov       [rbp-64],eax
       vxorps    xmm4,xmm4,xmm4
       vmovdqu   ymmword ptr [rbp-60],ymm4
       mov       [rbp-40],rax
       mov       [rbp+10],rcx
       mov       dword ptr [rbp-70],3E8
       mov       rax,[rbp+10]
       mov       rdx,[rax+38]
       lea       rcx,[rbp-60]
       xor       r8d,r8d
       call      qword ptr [7FFD706EDCE0]; Bshox.BshoxWriter..ctor(System.Buffers.IBufferWriter`1<Byte>, Bshox.BshoxOptions)
       xor       eax,eax
       mov       [rbp-64],eax
       jmp       short M00_L01
M00_L00:
       mov       rcx,7FFD70733AF0
       call      CORINFO_HELP_COUNTPROFILE32
       mov       rax,[rbp+10]
       mov       rax,[rax+8]
       mov       ecx,[rbp-64]
       cmp       ecx,[rax+8]
       jae       short M00_L03
       mov       edx,ecx
       lea       rax,[rax+rdx*4+10]
       movzx     edx,byte ptr [rax]
       lea       rcx,[rbp-60]
       call      qword ptr [7FFD706EDCF8]; Bshox.BshoxWriter.WriteByte(Byte)
       mov       eax,[rbp-64]
       inc       eax
       mov       [rbp-64],eax
M00_L01:
       mov       eax,[rbp-70]
       dec       eax
       mov       [rbp-70],eax
       cmp       dword ptr [rbp-70],0
       jg        short M00_L02
       lea       rcx,[rbp-70]
       mov       edx,26
       call      CORINFO_HELP_PATCHPOINT
M00_L02:
       cmp       dword ptr [rbp-64],3E8
       jl        short M00_L00
       mov       rcx,7FFD70733AF4
       call      CORINFO_HELP_COUNTPROFILE32
       mov       rax,[rbp+10]
       mov       rcx,[rax+38]
       cmp       [rcx],ecx
       call      qword ptr [7FFD706EDD10]; Bshox.TestUtils.FixedBufferWriter.Reset()
       lea       rcx,[rbp-60]
       call      qword ptr [7FFD706EDD28]; Bshox.BshoxWriter.get_UnflushedBytes()
       nop
       add       rsp,90
       pop       rbp
       ret
M00_L03:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 226
```
```asm
; Bshox.BshoxWriter..ctor(System.Buffers.IBufferWriter`1<Byte>, Bshox.BshoxOptions)
       push      rbp
       sub       rsp,30
       lea       rbp,[rsp+30]
       xor       eax,eax
       mov       [rbp-8],rax
       mov       [rbp-10],rax
       mov       [rbp+10],rcx
       mov       [rbp+18],rdx
       mov       [rbp+20],r8
       mov       rax,[rbp+10]
       xor       ecx,ecx
       mov       [rax+10],rcx
       mov       rax,[rbp+10]
       xor       ecx,ecx
       mov       [rax+18],ecx
       mov       rax,[rbp+10]
       xor       ecx,ecx
       mov       [rax+1C],ecx
       mov       rax,[rbp+10]
       xor       ecx,ecx
       mov       [rax+20],ecx
       mov       rax,[rbp+10]
       mov       rcx,[rbp+18]
       mov       [rax],rcx
       mov       rax,[rbp+10]
       mov       [rbp-8],rax
       mov       rax,[rbp+20]
       mov       [rbp-10],rax
       cmp       qword ptr [rbp+20],0
       jne       short M01_L00
       mov       rcx,offset MT_Bshox.BshoxOptions
       call      qword ptr [7FFD70385728]; System.Runtime.CompilerServices.StaticsHelpers.GetGCStaticBase(System.Runtime.CompilerServices.MethodTable*)
       mov       rax,254F8001218
       mov       rax,[rax]
       mov       [rbp-10],rax
M01_L00:
       mov       rax,[rbp-8]
       mov       rcx,[rbp-10]
       mov       [rax+8],rcx
       add       rsp,30
       pop       rbp
       ret
; Total bytes of code 154
```
```asm
; Bshox.BshoxWriter.WriteByte(Byte)
       push      rbp
       sub       rsp,20
       lea       rbp,[rsp+20]
       mov       [rbp+10],rcx
       mov       [rbp+18],edx
       mov       rcx,[rbp+10]
       mov       edx,1
       call      qword ptr [7FFD706EDDB8]; Bshox.BshoxWriter.GetRef(Int32)
       mov       ecx,[rbp+18]
       mov       [rax],cl
       mov       rcx,[rbp+10]
       mov       edx,1
       call      qword ptr [7FFD706EDDD0]; Bshox.BshoxWriter.Advance(Int32)
       nop
       add       rsp,20
       pop       rbp
       ret
; Total bytes of code 59
```
```asm
; Bshox.TestUtils.FixedBufferWriter.Reset()
       push      rbp
       push      rdi
       push      rsi
       lea       rbp,[rsp+10]
       mov       [rbp+10],rcx
       mov       rax,[rbp+10]
       lea       rsi,[rax+18]
       mov       rax,[rbp+10]
       lea       rdi,[rax+8]
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       pop       rsi
       pop       rdi
       pop       rbp
       ret
; Total bytes of code 39
```
```asm
; Bshox.BshoxWriter.get_UnflushedBytes()
       push      rbp
       mov       rbp,rsp
       mov       [rbp+10],rcx
       mov       rax,[rbp+10]
       mov       eax,[rax+1C]
       pop       rbp
       ret
; Total bytes of code 17
```

## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: Dry(IterationCount=1, LaunchCount=25, RunStrategy=ColdStart, UnrollFactor=1, WarmupCount=1))

```asm
; Benchmark.WriteVarInt.Write1()
       push      rbp
       sub       rsp,90
       lea       rbp,[rsp+90]
       xor       eax,eax
       mov       [rbp-64],eax
       vxorps    xmm4,xmm4,xmm4
       vmovdqu   ymmword ptr [rbp-60],ymm4
       mov       [rbp-40],rax
       mov       [rbp+10],rcx
       mov       dword ptr [rbp-70],3E8
       mov       rax,[rbp+10]
       mov       rdx,[rax+38]
       lea       rcx,[rbp-60]
       xor       r8d,r8d
       call      qword ptr [7FFD706DDCE0]; Bshox.BshoxWriter..ctor(System.Buffers.IBufferWriter`1<Byte>, Bshox.BshoxOptions)
       xor       eax,eax
       mov       [rbp-64],eax
       jmp       short M00_L01
M00_L00:
       mov       rcx,7FFD70723AF0
       call      CORINFO_HELP_COUNTPROFILE32
       mov       rax,[rbp+10]
       mov       rax,[rax+8]
       mov       ecx,[rbp-64]
       cmp       ecx,[rax+8]
       jae       short M00_L03
       mov       edx,ecx
       lea       rax,[rax+rdx*4+10]
       mov       edx,[rax]
       lea       rcx,[rbp-60]
       call      qword ptr [7FFD706DDCF8]; Bshox.BshoxWriter.WriteVarInt32(UInt32)
       mov       eax,[rbp-64]
       inc       eax
       mov       [rbp-64],eax
M00_L01:
       mov       eax,[rbp-70]
       dec       eax
       mov       [rbp-70],eax
       cmp       dword ptr [rbp-70],0
       jg        short M00_L02
       lea       rcx,[rbp-70]
       mov       edx,25
       call      CORINFO_HELP_PATCHPOINT
M00_L02:
       cmp       dword ptr [rbp-64],3E8
       jl        short M00_L00
       mov       rcx,7FFD70723AF4
       call      CORINFO_HELP_COUNTPROFILE32
       mov       rax,[rbp+10]
       mov       rcx,[rax+38]
       cmp       [rcx],ecx
       call      qword ptr [7FFD706DDD10]; Bshox.TestUtils.FixedBufferWriter.Reset()
       lea       rcx,[rbp-60]
       call      qword ptr [7FFD706DDD28]; Bshox.BshoxWriter.get_UnflushedBytes()
       nop
       add       rsp,90
       pop       rbp
       ret
M00_L03:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 225
```
```asm
; Bshox.BshoxWriter..ctor(System.Buffers.IBufferWriter`1<Byte>, Bshox.BshoxOptions)
       push      rbp
       sub       rsp,30
       lea       rbp,[rsp+30]
       xor       eax,eax
       mov       [rbp-8],rax
       mov       [rbp-10],rax
       mov       [rbp+10],rcx
       mov       [rbp+18],rdx
       mov       [rbp+20],r8
       mov       rax,[rbp+10]
       xor       ecx,ecx
       mov       [rax+10],rcx
       mov       rax,[rbp+10]
       xor       ecx,ecx
       mov       [rax+18],ecx
       mov       rax,[rbp+10]
       xor       ecx,ecx
       mov       [rax+1C],ecx
       mov       rax,[rbp+10]
       xor       ecx,ecx
       mov       [rax+20],ecx
       mov       rax,[rbp+10]
       mov       rcx,[rbp+18]
       mov       [rax],rcx
       mov       rax,[rbp+10]
       mov       [rbp-8],rax
       mov       rax,[rbp+20]
       mov       [rbp-10],rax
       cmp       qword ptr [rbp+20],0
       jne       short M01_L00
       mov       rcx,offset MT_Bshox.BshoxOptions
       call      qword ptr [7FFD70375728]; System.Runtime.CompilerServices.StaticsHelpers.GetGCStaticBase(System.Runtime.CompilerServices.MethodTable*)
       mov       rax,1F6F9001218
       mov       rax,[rax]
       mov       [rbp-10],rax
M01_L00:
       mov       rax,[rbp-8]
       mov       rcx,[rbp-10]
       mov       [rax+8],rcx
       add       rsp,30
       pop       rbp
       ret
; Total bytes of code 154
```
```asm
; Bshox.BshoxWriter.WriteVarInt32(UInt32)
       push      rbp
       sub       rsp,70
       lea       rbp,[rsp+70]
       xor       eax,eax
       mov       [rbp-40],rax
       mov       [rbp-44],eax
       mov       [rbp+10],rcx
       mov       [rbp+18],edx
       mov       dword ptr [rbp-50],3E8
       mov       rcx,[rbp+10]
       mov       edx,5
       call      qword ptr [7FFD706DDDD0]; Bshox.BshoxWriter.GetRef(Int32)
       mov       [rbp-40],rax
       xor       eax,eax
       mov       [rbp-44],eax
       jmp       short M02_L01
M02_L00:
       mov       rcx,7FFD70723FE8
       call      CORINFO_HELP_COUNTPROFILE32
       mov       eax,[rbp-44]
       mov       [rbp-48],eax
       mov       eax,[rbp-44]
       inc       eax
       mov       [rbp-44],eax
       mov       eax,[rbp+18]
       or        eax,0FFFFFF80
       movsxd    rcx,dword ptr [rbp-48]
       mov       rdx,[rbp-40]
       mov       [rdx+rcx],al
       mov       eax,[rbp+18]
       shr       eax,7
       mov       [rbp+18],eax
M02_L01:
       mov       eax,[rbp-50]
       dec       eax
       mov       [rbp-50],eax
       cmp       dword ptr [rbp-50],0
       jg        short M02_L02
       lea       rcx,[rbp-50]
       mov       edx,22
       call      CORINFO_HELP_PATCHPOINT
M02_L02:
       cmp       dword ptr [rbp+18],7F
       ja        short M02_L00
       mov       rcx,7FFD70723FEC
       call      CORINFO_HELP_COUNTPROFILE32
       movsxd    rax,dword ptr [rbp-44]
       mov       rcx,[rbp-40]
       mov       edx,[rbp+18]
       mov       [rcx+rax],dl
       mov       eax,[rbp-44]
       lea       edx,[rax+1]
       mov       rcx,[rbp+10]
       call      qword ptr [7FFD706DDDE8]; Bshox.BshoxWriter.Advance(Int32)
       nop
       add       rsp,70
       pop       rbp
       ret
; Total bytes of code 200
```
```asm
; Bshox.TestUtils.FixedBufferWriter.Reset()
       push      rbp
       push      rdi
       push      rsi
       lea       rbp,[rsp+10]
       mov       [rbp+10],rcx
       mov       rax,[rbp+10]
       lea       rsi,[rax+18]
       mov       rax,[rbp+10]
       lea       rdi,[rax+8]
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       pop       rsi
       pop       rdi
       pop       rbp
       ret
; Total bytes of code 39
```
```asm
; Bshox.BshoxWriter.get_UnflushedBytes()
       push      rbp
       mov       rbp,rsp
       mov       [rbp+10],rcx
       mov       rax,[rbp+10]
       mov       eax,[rax+1C]
       pop       rbp
       ret
; Total bytes of code 17
```

## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: Dry(IterationCount=1, LaunchCount=25, RunStrategy=ColdStart, UnrollFactor=1, WarmupCount=1))

```asm
; Benchmark.WriteVarInt.WriteAny()
       push      rbp
       sub       rsp,90
       lea       rbp,[rsp+90]
       xor       eax,eax
       mov       [rbp-64],eax
       vxorps    xmm4,xmm4,xmm4
       vmovdqu   ymmword ptr [rbp-60],ymm4
       mov       [rbp-40],rax
       mov       [rbp+10],rcx
       mov       dword ptr [rbp-70],3E8
       mov       rax,[rbp+10]
       mov       rdx,[rax+38]
       lea       rcx,[rbp-60]
       xor       r8d,r8d
       call      qword ptr [7FFD7070DCE0]; Bshox.BshoxWriter..ctor(System.Buffers.IBufferWriter`1<Byte>, Bshox.BshoxOptions)
       xor       eax,eax
       mov       [rbp-64],eax
       jmp       short M00_L01
M00_L00:
       mov       rcx,7FFD70753AF0
       call      CORINFO_HELP_COUNTPROFILE32
       mov       rax,[rbp+10]
       mov       rax,[rax+30]
       mov       ecx,[rbp-64]
       cmp       ecx,[rax+8]
       jae       short M00_L03
       mov       edx,ecx
       lea       rax,[rax+rdx*4+10]
       mov       edx,[rax]
       lea       rcx,[rbp-60]
       call      qword ptr [7FFD7070DCF8]; Bshox.BshoxWriter.WriteVarInt32(UInt32)
       mov       eax,[rbp-64]
       inc       eax
       mov       [rbp-64],eax
M00_L01:
       mov       eax,[rbp-70]
       dec       eax
       mov       [rbp-70],eax
       cmp       dword ptr [rbp-70],0
       jg        short M00_L02
       lea       rcx,[rbp-70]
       mov       edx,25
       call      CORINFO_HELP_PATCHPOINT
M00_L02:
       cmp       dword ptr [rbp-64],3E8
       jl        short M00_L00
       mov       rcx,7FFD70753AF4
       call      CORINFO_HELP_COUNTPROFILE32
       mov       rax,[rbp+10]
       mov       rcx,[rax+38]
       cmp       [rcx],ecx
       call      qword ptr [7FFD7070DD10]; Bshox.TestUtils.FixedBufferWriter.Reset()
       lea       rcx,[rbp-60]
       call      qword ptr [7FFD7070DD28]; Bshox.BshoxWriter.get_UnflushedBytes()
       nop
       add       rsp,90
       pop       rbp
       ret
M00_L03:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 225
```
```asm
; Bshox.BshoxWriter..ctor(System.Buffers.IBufferWriter`1<Byte>, Bshox.BshoxOptions)
       push      rbp
       sub       rsp,30
       lea       rbp,[rsp+30]
       xor       eax,eax
       mov       [rbp-8],rax
       mov       [rbp-10],rax
       mov       [rbp+10],rcx
       mov       [rbp+18],rdx
       mov       [rbp+20],r8
       mov       rax,[rbp+10]
       xor       ecx,ecx
       mov       [rax+10],rcx
       mov       rax,[rbp+10]
       xor       ecx,ecx
       mov       [rax+18],ecx
       mov       rax,[rbp+10]
       xor       ecx,ecx
       mov       [rax+1C],ecx
       mov       rax,[rbp+10]
       xor       ecx,ecx
       mov       [rax+20],ecx
       mov       rax,[rbp+10]
       mov       rcx,[rbp+18]
       mov       [rax],rcx
       mov       rax,[rbp+10]
       mov       [rbp-8],rax
       mov       rax,[rbp+20]
       mov       [rbp-10],rax
       cmp       qword ptr [rbp+20],0
       jne       short M01_L00
       mov       rcx,offset MT_Bshox.BshoxOptions
       call      qword ptr [7FFD703A5728]; System.Runtime.CompilerServices.StaticsHelpers.GetGCStaticBase(System.Runtime.CompilerServices.MethodTable*)
       mov       rax,23607001218
       mov       rax,[rax]
       mov       [rbp-10],rax
M01_L00:
       mov       rax,[rbp-8]
       mov       rcx,[rbp-10]
       mov       [rax+8],rcx
       add       rsp,30
       pop       rbp
       ret
; Total bytes of code 154
```
```asm
; Bshox.BshoxWriter.WriteVarInt32(UInt32)
       push      rbp
       sub       rsp,70
       lea       rbp,[rsp+70]
       xor       eax,eax
       mov       [rbp-40],rax
       mov       [rbp-44],eax
       mov       [rbp+10],rcx
       mov       [rbp+18],edx
       mov       dword ptr [rbp-50],3E8
       mov       rcx,[rbp+10]
       mov       edx,5
       call      qword ptr [7FFD7070DDD0]; Bshox.BshoxWriter.GetRef(Int32)
       mov       [rbp-40],rax
       xor       eax,eax
       mov       [rbp-44],eax
       jmp       short M02_L01
M02_L00:
       mov       rcx,7FFD70753FE8
       call      CORINFO_HELP_COUNTPROFILE32
       mov       eax,[rbp-44]
       mov       [rbp-48],eax
       mov       eax,[rbp-44]
       inc       eax
       mov       [rbp-44],eax
       mov       eax,[rbp+18]
       or        eax,0FFFFFF80
       movsxd    rcx,dword ptr [rbp-48]
       mov       rdx,[rbp-40]
       mov       [rdx+rcx],al
       mov       eax,[rbp+18]
       shr       eax,7
       mov       [rbp+18],eax
M02_L01:
       mov       eax,[rbp-50]
       dec       eax
       mov       [rbp-50],eax
       cmp       dword ptr [rbp-50],0
       jg        short M02_L02
       lea       rcx,[rbp-50]
       mov       edx,22
       call      CORINFO_HELP_PATCHPOINT
M02_L02:
       cmp       dword ptr [rbp+18],7F
       ja        short M02_L00
       mov       rcx,7FFD70753FEC
       call      CORINFO_HELP_COUNTPROFILE32
       movsxd    rax,dword ptr [rbp-44]
       mov       rcx,[rbp-40]
       mov       edx,[rbp+18]
       mov       [rcx+rax],dl
       mov       eax,[rbp-44]
       lea       edx,[rax+1]
       mov       rcx,[rbp+10]
       call      qword ptr [7FFD7070DDE8]; Bshox.BshoxWriter.Advance(Int32)
       nop
       add       rsp,70
       pop       rbp
       ret
; Total bytes of code 200
```
```asm
; Bshox.TestUtils.FixedBufferWriter.Reset()
       push      rbp
       push      rdi
       push      rsi
       lea       rbp,[rsp+10]
       mov       [rbp+10],rcx
       mov       rax,[rbp+10]
       lea       rsi,[rax+18]
       mov       rax,[rbp+10]
       lea       rdi,[rax+8]
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       pop       rsi
       pop       rdi
       pop       rbp
       ret
; Total bytes of code 39
```
```asm
; Bshox.BshoxWriter.get_UnflushedBytes()
       push      rbp
       mov       rbp,rsp
       mov       [rbp+10],rcx
       mov       rax,[rbp+10]
       mov       eax,[rax+1C]
       pop       rbp
       ret
; Total bytes of code 17
```