## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: DefaultJob)

```asm
; Benchmark.GuidContract.Serialize()
;         var writer = new BshoxWriter(fixedBufferWriter);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         contract.Serialize(ref writer, in _guid);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         fixedBufferWriter.Reset();
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^
;         return writer.UnflushedBytes;
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       push      rdi
       push      rsi
       sub       rsp,48
       vxorps    xmm4,xmm4,xmm4
       vmovdqu   ymmword ptr [rsp+20],ymm4
       xor       eax,eax
       mov       [rsp+40],rax
       mov       rsi,rcx
       mov       rdx,[rsi+8]
       mov       [rsp+20],rdx
       mov       rdx,277ACC003F0
       mov       rdx,[rdx]
       mov       [rsp+28],rdx
       mov       rcx,[rsi+10]
       lea       r8,[rsi+20]
       mov       rdx,offset MT_Bshox.DefaultContracts+GuidContract
       cmp       [rcx],rdx
       jne       short M00_L01
       lea       rdx,[rsp+20]
       call      qword ptr [7FFC5458B0D8]; Bshox.DefaultContracts+GuidContract.Serialize(Bshox.BshoxWriter ByRef, System.Guid ByRef)
M00_L00:
       mov       rdi,[rsi+8]
       lea       rsi,[rdi+18]
       add       rdi,8
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,[rsp+3C]
       add       rsp,48
       pop       rsi
       pop       rdi
       ret
M00_L01:
       lea       rdx,[rsp+20]
       mov       rax,[rcx]
       mov       rax,[rax+48]
       call      qword ptr [rax]
       jmp       short M00_L00
; Total bytes of code 133
```
```asm
; Bshox.DefaultContracts+GuidContract.Serialize(Bshox.BshoxWriter ByRef, System.Guid ByRef)
;             ref byte bytes = ref writer.GetRef(sizeofGuid + 1);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;             bytes = sizeofGuid;
;             ^^^^^^^^^^^^^^^^^^^
;                 Unsafe.WriteUnaligned(ref Unsafe.Add(ref bytes, 1), EndiannessHelper.Reverse(value));
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;             writer.Advance(sizeofGuid + 1);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,68
       vxorps    xmm4,xmm4,xmm4
       vmovdqu   ymmword ptr [rsp+30],ymm4
       vmovdqa   xmmword ptr [rsp+50],xmm4
       mov       rax,4B11ED1F81EE
       mov       [rsp+60],rax
       mov       rbx,r8
       mov       rsi,rdx
       cmp       dword ptr [rsi+18],11
       jge       near ptr M01_L04
       cmp       dword ptr [rsi+1C],0
       jg        near ptr M01_L06
M01_L00:
       mov       rcx,[rsi]
       mov       rdx,[rsi+8]
       mov       edx,[rdx+0C]
       mov       r8d,11
       cmp       edx,11
       cmovg     r8d,edx
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rcx],rdx
       jne       near ptr M01_L09
       lea       rdx,[rsp+40]
       call      qword ptr [7FFC54586930]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       edi,edi
       xor       ebp,ebp
       mov       rcx,[rsp+40]
       test      rcx,rcx
       je        short M01_L02
       mov       rdx,[rcx]
       test      dword ptr [rdx],80000000
       je        near ptr M01_L07
       lea       rdi,[rcx+10]
       mov       ebp,[rcx+8]
M01_L01:
       mov       eax,[rsp+48]
       and       eax,7FFFFFFF
       mov       ecx,[rsp+4C]
       mov       edx,ecx
       add       rdx,rax
       mov       r8d,ebp
       cmp       rdx,r8
       ja        near ptr M01_L08
       add       rdi,rax
       mov       ebp,ecx
M01_L02:
       mov       [rsp+50],rdi
       mov       [rsp+58],ebp
M01_L03:
       cmp       dword ptr [rsp+58],0
       jbe       near ptr M01_L10
       mov       rax,[rsp+50]
       mov       [rsi+10],rax
       mov       eax,[rsp+58]
       mov       [rsi+18],eax
M01_L04:
       mov       rax,[rsi+10]
       mov       byte ptr [rax],10
       mov       ecx,[rbx]
       movsx     rdx,word ptr [rbx+4]
       movsx     r8,word ptr [rbx+6]
       mov       r10,[rbx+8]
       movbe     [rax+1],ecx
       movbe     [rax+5],dx
       movbe     [rax+7],r8w
       mov       [rax+9],r10
       add       qword ptr [rsi+10],11
       add       dword ptr [rsi+18],0FFFFFFEF
       add       dword ptr [rsi+1C],11
       mov       r8,4B11ED1F81EE
       cmp       [rsp+60],r8
       je        short M01_L05
       call      CORINFO_HELP_FAIL_FAST
M01_L05:
       nop
       add       rsp,68
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M01_L06:
       mov       rcx,[rsi]
       mov       edx,[rsi+1C]
       mov       r11,7FFC542704B8
       call      qword ptr [r11]
       xor       edx,edx
       mov       [rsi+10],rdx
       mov       [rsi+18],edx
       mov       [rsi+1C],edx
       jmp       near ptr M01_L00
M01_L07:
       lea       rdx,[rsp+30]
       mov       rax,[rcx]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rdi,[rsp+30]
       mov       ebp,[rsp+38]
       jmp       near ptr M01_L01
M01_L08:
       call      qword ptr [7FFC544F7990]
       int       3
M01_L09:
       lea       rdx,[rsp+50]
       mov       r11,7FFC542704B0
       call      qword ptr [r11]
       jmp       near ptr M01_L03
M01_L10:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 416
```
```asm
; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
;         if (sizeHint <= _memory.Length)
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;             return _memory;
;             ^^^^^^^^^^^^^^^
;         throw Error(sizeHint, _memory.Length);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       sub       rsp,28
       cmp       r8d,[rcx+14]
       jg        short M02_L00
       vmovdqu   xmm0,xmmword ptr [rcx+8]
       vmovdqu   xmmword ptr [rdx],xmm0
       mov       rax,rdx
       add       rsp,28
       ret
M02_L00:
       mov       edx,[rcx+14]
       mov       ecx,r8d
       call      qword ptr [7FFC544FE580]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 48
```

## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: DefaultJob)

```asm
; Benchmark.GuidContract.Deserialize()
;         var reader = new BshoxReader(buffer);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         contract.Deserialize(ref reader, out Guid guid);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         return guid;
;         ^^^^^^^^^^^^
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,0A0
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqu   ymmword ptr [rsp+30],ymm4
       vmovdqu   ymmword ptr [rsp+50],ymm4
       vmovdqu   ymmword ptr [rsp+70],ymm4
       vmovdqa   xmmword ptr [rsp+90],xmm4
       mov       rbx,rcx
       mov       rsi,rdx
       mov       rcx,[rbx+18]
       test      rcx,rcx
       je        near ptr M00_L04
       mov       edi,[rcx+8]
M00_L00:
       vxorps    xmm0,xmm0,xmm0
       vmovdqu   xmmword ptr [rsp+68],xmm0
       vxorps    xmm0,xmm0,xmm0
       vmovdqu   xmmword ptr [rsp+78],xmm0
       vmovdqu   xmmword ptr [rsp+80],xmm0
       vxorps    xmm0,xmm0,xmm0
       vmovdqu   xmmword ptr [rsp+90],xmm0
       mov       byte ptr [rsp+64],0
       mov       byte ptr [rsp+65],0
       xor       edx,edx
       mov       [rsp+60],edx
       mov       [rsp+50],rdx
       mov       [rsp+58],rdx
       mov       rdx,11AF14003E8
       mov       rdx,[rdx]
       mov       [rsp+48],rdx
       xor       edx,edx
       mov       [rsp+50],rdx
       mov       byte ptr [rsp+64],0
       mov       edx,edi
       mov       [rsp+58],rdx
       xor       ebp,ebp
       xor       r14d,r14d
       test      rcx,rcx
       je        short M00_L02
       mov       rdx,[rcx]
       test      dword ptr [rdx],80000000
       je        short M00_L05
       lea       rbp,[rcx+10]
       mov       r14d,[rcx+8]
M00_L01:
       cmp       edi,r14d
       ja        near ptr M00_L06
       mov       r14d,edi
M00_L02:
       mov       [rsp+68],rbp
       mov       [rsp+70],r14d
       test      edi,edi
       setne     r8b
       movzx     r8d,r8b
       mov       [rsp+65],r8b
       mov       rcx,[rbx+10]
       mov       r8,offset MT_Bshox.DefaultContracts+GuidContract
       cmp       [rcx],r8
       jne       short M00_L07
       lea       r8,[rsp+38]
       lea       rdx,[rsp+48]
       call      qword ptr [7FFC5459ABE0]; Bshox.DefaultContracts+GuidContract.Deserialize(Bshox.BshoxReader ByRef, System.Guid ByRef)
M00_L03:
       vmovups   xmm0,[rsp+38]
       vmovups   [rsi],xmm0
       mov       rax,rsi
       add       rsp,0A0
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M00_L04:
       xor       ecx,ecx
       xor       edi,edi
       jmp       near ptr M00_L00
M00_L05:
       lea       rdx,[rsp+28]
       mov       rax,[rcx]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rbp,[rsp+28]
       mov       r14d,[rsp+30]
       jmp       near ptr M00_L01
M00_L06:
       call      qword ptr [7FFC54507990]
       int       3
M00_L07:
       lea       r8,[rsp+38]
       lea       rdx,[rsp+48]
       mov       rax,[rcx]
       mov       rax,[rax+48]
       call      qword ptr [rax+8]
       jmp       short M00_L03
; Total bytes of code 374
```
```asm
; Bshox.DefaultContracts+GuidContract.Deserialize(Bshox.BshoxReader ByRef, System.Guid ByRef)
;             byte length = reader.ReadByte();
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;             if (length != sizeofGuid)
;             ^^^^^^^^^^^^^^^^^^^^^^^^^
;                 throw new BshoxException($"Expected {sizeofGuid} bytes but got {length}");
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;             value = reader.ReadUnsafe<Guid>();
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;                 value = EndiannessHelper.Reverse(value);
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,88
       vmovaps   [rsp+70],xmm6
       vxorps    xmm4,xmm4,xmm4
       vmovdqu   ymmword ptr [rsp+40],ymm4
       xor       eax,eax
       mov       [rsp+60],rax
       mov       rax,44FF758016F4
       mov       [rsp+68],rax
       mov       rbx,r8
       mov       rsi,rdx
       cmp       byte ptr [rsi+1D],0
       je        near ptr M01_L15
       lea       rdi,[rsi+20]
       mov       rcx,rdi
       mov       eax,[rcx+8]
       test      eax,eax
       je        near ptr M01_L17
       mov       rcx,[rcx]
       movzx     ebp,byte ptr [rcx]
       inc       rcx
       dec       eax
       mov       [rsi+20],rcx
       mov       [rsi+28],eax
       inc       qword ptr [rsi+8]
       mov       rcx,rdi
       cmp       dword ptr [rcx+8],0
       je        near ptr M01_L04
M01_L00:
       cmp       ebp,10
       jne       near ptr M01_L06
       mov       ecx,[rsi+28]
       cmp       ecx,10
       jl        near ptr M01_L16
       mov       rdx,[rsi+20]
       vmovups   xmm6,[rdx]
       cmp       ecx,10
       jg        near ptr M01_L11
       cmp       byte ptr [rsi+1C],0
       jne       near ptr M01_L14
       cmp       ecx,10
       jne       near ptr M01_L15
       xor       ecx,ecx
       mov       [rdi],rcx
       mov       [rdi+8],rcx
       add       qword ptr [rsi+8],10
       mov       byte ptr [rsi+1D],0
M01_L01:
       vmovups   [rsp+30],xmm6
M01_L02:
       vmovups   xmm0,[rsp+30]
       vmovups   [rbx],xmm0
       mov       ecx,[rbx]
       movsx     rdx,word ptr [rbx+4]
       movsx     r8,word ptr [rbx+6]
       mov       rax,[rbx+8]
       movbe     [rbx],ecx
       movbe     [rbx+4],dx
       movbe     [rbx+6],r8w
       mov       [rbx+8],rax
       mov       r8,44FF758016F4
       cmp       [rsp+68],r8
       je        short M01_L03
       call      CORINFO_HELP_FAIL_FAST
M01_L03:
       nop
       vmovaps   xmm6,[rsp+70]
       add       rsp,88
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M01_L04:
       cmp       byte ptr [rsi+1C],0
       je        short M01_L05
       mov       rcx,rsi
       call      qword ptr [7FFC547143C0]
       jmp       near ptr M01_L00
M01_L05:
       mov       byte ptr [rsi+1D],0
       jmp       near ptr M01_L00
M01_L06:
       lea       rcx,[rsp+40]
       mov       edx,18
       mov       r8d,2
       call      qword ptr [7FFC5433C270]; System.Runtime.CompilerServices.DefaultInterpolatedStringHandler..ctor(Int32, Int32)
       mov       ecx,[rsp+50]
       cmp       ecx,[rsp+60]
       ja        near ptr M01_L12
       mov       rdx,[rsp+58]
       mov       eax,ecx
       lea       rdx,[rdx+rax*2]
       mov       eax,[rsp+60]
       sub       eax,ecx
       cmp       eax,9
       jb        short M01_L07
       vmovups   xmm0,[7FFC543AC740]
       vmovups   [rdx],xmm0
       mov       word ptr [rdx+10],20
       mov       ecx,[rsp+50]
       add       ecx,9
       mov       [rsp+50],ecx
       jmp       short M01_L08
M01_L07:
       lea       rcx,[rsp+40]
       mov       rdx,11AA51F8B98
       call      qword ptr [7FFC546A64F0]
M01_L08:
       lea       rcx,[rsp+40]
       mov       edx,10
       call      qword ptr [7FFC54337FD8]; System.Runtime.CompilerServices.DefaultInterpolatedStringHandler.AppendFormatted[[System.Int32, System.Private.CoreLib]](Int32)
       mov       ecx,[rsp+50]
       cmp       ecx,[rsp+60]
       ja        near ptr M01_L12
       mov       rdx,[rsp+58]
       mov       eax,ecx
       lea       rdx,[rdx+rax*2]
       mov       eax,[rsp+60]
       sub       eax,ecx
       cmp       eax,0F
       jb        short M01_L09
       vmovups   xmm0,[7FFC543AC750]
       vmovups   [rdx],xmm0
       mov       rcx,67002000740075
       mov       [rdx+10],rcx
       mov       dword ptr [rdx+18],74006F
       mov       word ptr [rdx+1C],20
       mov       ecx,[rsp+50]
       add       ecx,0F
       mov       [rsp+50],ecx
       jmp       short M01_L10
M01_L09:
       lea       rcx,[rsp+40]
       mov       rdx,11AA51F8BC0
       call      qword ptr [7FFC546A64F0]
M01_L10:
       lea       rcx,[rsp+40]
       mov       edx,ebp
       call      qword ptr [7FFC54714300]
       mov       rcx,offset MT_Bshox.BshoxException
       call      CORINFO_HELP_NEWSFAST
       mov       rbx,rax
       lea       rcx,[rsp+40]
       call      qword ptr [7FFC5433C2A0]; System.Runtime.CompilerServices.DefaultInterpolatedStringHandler.ToStringAndClear()
       mov       rsi,rax
       mov       rcx,rbx
       call      qword ptr [7FFC54716658]
       lea       rcx,[rbx+10]
       mov       rdx,rsi
       call      CORINFO_HELP_ASSIGN_REF
       mov       rcx,rbx
       call      CORINFO_HELP_THROW
       int       3
M01_L11:
       cmp       dword ptr [rdi+8],10
       jae       short M01_L13
M01_L12:
       call      qword ptr [7FFC54507990]
       int       3
M01_L13:
       mov       rdx,[rdi]
       add       rdx,10
       mov       ecx,[rdi+8]
       add       ecx,0FFFFFFF0
       mov       [rsi+20],rdx
       mov       [rsi+28],ecx
       add       qword ptr [rsi+8],10
       jmp       near ptr M01_L01
M01_L14:
       mov       rcx,rsi
       mov       edx,10
       call      qword ptr [7FFC54714468]
       jmp       near ptr M01_L01
M01_L15:
       call      qword ptr [7FFC54714360]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M01_L16:
       lea       rdx,[rsp+30]
       mov       rcx,rsi
       call      qword ptr [7FFC547143D8]
       jmp       near ptr M01_L02
M01_L17:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 729
```
```asm
; System.Runtime.CompilerServices.DefaultInterpolatedStringHandler..ctor(Int32, Int32)
       push      rdi
       push      rsi
       push      rbx
       sub       rsp,20
       mov       rbx,rcx
       mov       esi,edx
       mov       edi,r8d
       xor       eax,eax
       mov       [rbx],rax
       call      qword ptr [7FFC94565098]
       mov       rcx,[rax]
       imul      edx,edi,0B
       add       edx,esi
       mov       eax,100
       cmp       edx,100
       cmovle    edx,eax
       cmp       [rcx],ecx
       call      qword ptr [7FFC94584888]; Precode of System.Buffers.SharedArrayPool`1[[System.Char, System.Private.CoreLib]].Rent(Int32)
       mov       [rbx+8],rax
       test      rax,rax
       je        short M02_L01
       lea       rcx,[rax+10]
       mov       eax,[rax+8]
M02_L00:
       mov       [rbx+18],rcx
       mov       [rbx+20],eax
       xor       eax,eax
       mov       [rbx+10],eax
       mov       byte ptr [rbx+14],0
       add       rsp,20
       pop       rbx
       pop       rsi
       pop       rdi
       ret
M02_L01:
       xor       ecx,ecx
       xor       eax,eax
       jmp       short M02_L00
; Total bytes of code 102
```
```asm
; System.Runtime.CompilerServices.DefaultInterpolatedStringHandler.AppendFormatted[[System.Int32, System.Private.CoreLib]](Int32)
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,50
       xor       eax,eax
       mov       [rsp+28],rax
       xorps     xmm4,xmm4
       movaps    [rsp+30],xmm4
       mov       [rsp+40],rax
       mov       rbx,rcx
       mov       esi,edx
       cmp       byte ptr [rbx+14],0
       jne       short M03_L03
       lea       rdi,[rbx+18]
       mov       ebp,[rbx+10]
       mov       r14d,[rdi+8]
       cmp       ebp,r14d
       ja        near ptr M03_L10
M03_L00:
       mov       rdx,[rdi]
       mov       r8d,ebp
       lea       rdi,[rdx+r8*2]
       sub       r14d,ebp
       mov       rcx,[rbx]
       test      esi,esi
       jl        short M03_L05
       mov       [rsp+38],rdi
       mov       [rsp+40],r14d
       lea       rdx,[rsp+38]
       lea       r8,[rsp+48]
       mov       ecx,esi
       call      qword ptr [7FFC9458BCD8]; Precode of System.Number.TryUInt32ToDecStr[[System.Char, System.Private.CoreLib]](UInt32, System.Span`1<Char>, Int32 ByRef)
M03_L01:
       test      eax,eax
       je        short M03_L04
       mov       eax,[rsp+48]
       add       [rbx+10],eax
M03_L02:
       add       rsp,50
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M03_L03:
       mov       rcx,rbx
       mov       edx,esi
       xor       r8d,r8d
       call      qword ptr [7FFC9458CB28]
       jmp       short M03_L02
M03_L04:
       mov       rcx,rbx
       call      qword ptr [7FFC9457FFD8]
       lea       rdi,[rbx+18]
       mov       ebp,[rbx+10]
       mov       r14d,[rdi+8]
       cmp       ebp,r14d
       jbe       short M03_L00
       jmp       short M03_L10
M03_L05:
       test      rcx,rcx
       je        short M03_L06
       call      qword ptr [7FFC9457C230]; Precode of System.Globalization.NumberFormatInfo.<GetInstance>g__GetProviderNonNull|58_0(System.IFormatProvider)
       jmp       short M03_L07
M03_L06:
       call      qword ptr [7FFC9457C218]; Precode of System.Globalization.NumberFormatInfo.get_CurrentInfo()
M03_L07:
       mov       r8,[rax+28]
       test      r8,r8
       jne       short M03_L08
       xor       r9d,r9d
       xor       r8d,r8d
       jmp       short M03_L09
M03_L08:
       lea       r9,[r8+0C]
       mov       r8d,[r8+8]
M03_L09:
       mov       [rsp+28],r9
       mov       [rsp+30],r8d
       mov       [rsp+38],rdi
       mov       [rsp+40],r14d
       lea       r8,[rsp+48]
       mov       [rsp+20],r8
       lea       r8,[rsp+28]
       lea       r9,[rsp+38]
       mov       ecx,esi
       mov       edx,0FFFFFFFF
       call      qword ptr [7FFC9458BCC0]
       jmp       near ptr M03_L01
M03_L10:
       call      qword ptr [7FFC9457A278]
       int       3
; Total bytes of code 283
```
```asm
; System.Runtime.CompilerServices.DefaultInterpolatedStringHandler.ToStringAndClear()
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,38
       xor       eax,eax
       mov       [rsp+28],rax
       mov       rbx,rcx
       lea       rsi,[rbx+18]
       mov       rcx,rsi
       mov       eax,[rbx+10]
       cmp       eax,[rcx+8]
       ja        short M04_L01
       mov       rcx,[rcx]
       mov       [rsp+28],rcx
       mov       [rsp+30],eax
       lea       rcx,[rsp+28]
       call      qword ptr [7FFC94576B10]; Precode of System.String.Ctor(System.ReadOnlySpan`1<Char>)
       mov       rdi,rax
       mov       rbp,[rbx+8]
       xor       eax,eax
       mov       [rbx+8],rax
       mov       [rsi],rax
       mov       [rsi+8],rax
       mov       [rbx+10],eax
       test      rbp,rbp
       je        short M04_L00
       call      qword ptr [7FFC94565098]
       mov       rcx,[rax]
       mov       rdx,rbp
       xor       r8d,r8d
       cmp       [rcx],ecx
       call      qword ptr [7FFC94584890]; Precode of System.Buffers.SharedArrayPool`1[[System.Char, System.Private.CoreLib]].Return(Char[], Boolean)
M04_L00:
       mov       rax,rdi
       add       rsp,38
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M04_L01:
       call      qword ptr [7FFC9457A278]
       int       3
; Total bytes of code 126
```