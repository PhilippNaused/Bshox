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
       mov       rdx,1D13F4003F0
       mov       rdx,[rdx]
       mov       [rsp+28],rdx
       mov       rcx,[rsi+10]
       lea       r8,[rsi+20]
       mov       rdx,offset MT_Bshox.DefaultContracts+GuidContract
       cmp       [rcx],rdx
       jne       short M00_L01
       lea       rdx,[rsp+20]
       call      qword ptr [7FFC545BB0F8]; Bshox.DefaultContracts+GuidContract.Serialize(Bshox.BshoxWriter ByRef, System.Guid ByRef)
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
;             guid = value;
;             ^^^^^^^^^^^^^
;                 EndiannessHelper.Reverse(ref guid);
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;             writer.Advance(sizeofGuid + 1);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       push      r15
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,48
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       [rsp+40],rax
       mov       rbx,rdx
       mov       rsi,r8
       cmp       dword ptr [rbx+18],11
       jge       near ptr M01_L04
       cmp       dword ptr [rbx+1C],0
       jg        near ptr M01_L05
M01_L00:
       mov       rcx,[rbx]
       mov       rdx,[rbx+8]
       mov       edx,[rdx+0C]
       mov       r8d,11
       cmp       edx,11
       cmovg     r8d,edx
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rcx],rdx
       jne       near ptr M01_L09
       mov       edx,[rcx+14]
       mov       edi,edx
       cmp       r8d,edi
       jg        near ptr M01_L06
       mov       rax,[rcx+8]
       mov       ebp,[rcx+10]
       xor       r14d,r14d
       xor       r15d,r15d
       test      rax,rax
       je        short M01_L02
       mov       rdx,[rax]
       test      dword ptr [rdx],80000000
       je        near ptr M01_L07
       lea       r14,[rax+10]
       mov       r15d,[rax+8]
M01_L01:
       and       ebp,7FFFFFFF
       mov       eax,ebp
       mov       ecx,edi
       add       rcx,rax
       mov       edx,r15d
       cmp       rcx,rdx
       ja        near ptr M01_L08
       add       r14,rax
       mov       r15d,edi
M01_L02:
       mov       [rsp+38],r14
       mov       [rsp+40],r15d
M01_L03:
       cmp       dword ptr [rsp+40],0
       jbe       near ptr M01_L10
       mov       rax,[rsp+38]
       mov       [rbx+10],rax
       mov       eax,[rsp+40]
       mov       [rbx+18],eax
M01_L04:
       mov       rax,[rbx+10]
       mov       byte ptr [rax],10
       inc       rax
       vmovups   xmm0,[rsi]
       vmovups   [rax],xmm0
       mov       ecx,[rax]
       movbe     [rax],ecx
       movsx     rcx,word ptr [rax+4]
       movbe     [rax+4],cx
       movsx     rcx,word ptr [rax+6]
       movbe     [rax+6],cx
       add       qword ptr [rbx+10],11
       add       dword ptr [rbx+18],0FFFFFFEF
       add       dword ptr [rbx+1C],11
       add       rsp,48
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       pop       r15
       ret
M01_L05:
       mov       rcx,[rbx]
       mov       edx,[rbx+1C]
       mov       r11,7FFC542A04B8
       call      qword ptr [r11]
       xor       edx,edx
       mov       [rbx+10],rdx
       mov       [rbx+18],edx
       mov       [rbx+1C],edx
       jmp       near ptr M01_L00
M01_L06:
       mov       edx,edi
       mov       ecx,r8d
       call      qword ptr [7FFC5452E580]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M01_L07:
       lea       rdx,[rsp+28]
       mov       rcx,rax
       mov       rax,[rax]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       r14,[rsp+28]
       mov       r15d,[rsp+30]
       jmp       near ptr M01_L01
M01_L08:
       call      qword ptr [7FFC54527990]
       int       3
M01_L09:
       lea       rdx,[rsp+38]
       mov       r11,7FFC542A04B0
       call      qword ptr [r11]
       jmp       near ptr M01_L03
M01_L10:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 422
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
       je        near ptr M00_L06
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
       mov       rdx,1E1F88003E8
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
       je        near ptr M00_L07
       lea       rbp,[rcx+10]
       mov       r14d,[rcx+8]
M00_L01:
       cmp       edi,r14d
       ja        near ptr M00_L12
       mov       r14d,edi
M00_L02:
       mov       [rsp+68],rbp
       mov       [rsp+70],r14d
       test      edi,edi
       setne     cl
       movzx     ecx,cl
       mov       [rsp+65],cl
       mov       rcx,[rbx+10]
       mov       rax,offset MT_Bshox.DefaultContracts+GuidContract
       cmp       [rcx],rax
       jne       near ptr M00_L16
       cmp       byte ptr [rsp+65],0
       je        near ptr M00_L14
       mov       ecx,[rsp+70]
       test      ecx,ecx
       je        near ptr M00_L17
       mov       rax,[rsp+68]
       movzx     ebx,byte ptr [rax]
       inc       rax
       dec       ecx
       mov       [rsp+68],rax
       mov       [rsp+70],ecx
       mov       rcx,[rsp+50]
       inc       rcx
       mov       [rsp+50],rcx
       cmp       dword ptr [rsp+70],0
       je        near ptr M00_L08
M00_L03:
       mov       ecx,ebx
       cmp       ecx,10
       jne       near ptr M00_L10
       cmp       dword ptr [rsp+70],10
       jl        near ptr M00_L15
       mov       rcx,[rsp+68]
       vmovups   xmm0,[rcx]
       vmovups   [rsp+38],xmm0
       mov       ebx,[rsp+70]
       cmp       ebx,10
       jg        near ptr M00_L11
       cmp       byte ptr [rsp+64],0
       jne       near ptr M00_L13
       cmp       ebx,10
       jne       near ptr M00_L14
       vxorps    xmm0,xmm0,xmm0
       vmovdqu   xmmword ptr [rsp+68],xmm0
       mov       rcx,[rsp+50]
       add       rcx,10
       mov       [rsp+50],rcx
       mov       byte ptr [rsp+65],0
M00_L04:
       movbe     ecx,[rsp+38]
       mov       [rsp+38],ecx
       movbe     cx,[rsp+3C]
       movzx     ecx,cx
       mov       [rsp+3C],cx
       movbe     cx,[rsp+3E]
       movzx     ecx,cx
       mov       [rsp+3E],cx
M00_L05:
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
M00_L06:
       xor       ecx,ecx
       xor       edi,edi
       jmp       near ptr M00_L00
M00_L07:
       lea       rdx,[rsp+28]
       mov       rax,[rcx]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rbp,[rsp+28]
       mov       r14d,[rsp+30]
       jmp       near ptr M00_L01
M00_L08:
       cmp       byte ptr [rsp+64],0
       je        short M00_L09
       lea       rcx,[rsp+48]
       call      qword ptr [7FFC547442B8]
       jmp       near ptr M00_L03
M00_L09:
       mov       byte ptr [rsp+65],0
       jmp       near ptr M00_L03
M00_L10:
       call      qword ptr [7FFC54744240]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M00_L11:
       mov       rcx,[rsp+68]
       add       rcx,10
       add       ebx,0FFFFFFF0
       mov       [rsp+68],rcx
       mov       [rsp+70],ebx
       mov       rcx,[rsp+50]
       add       rcx,10
       mov       [rsp+50],rcx
       jmp       near ptr M00_L04
M00_L12:
       call      qword ptr [7FFC54527990]
       int       3
M00_L13:
       lea       rcx,[rsp+48]
       mov       edx,10
       call      qword ptr [7FFC54744348]
       jmp       near ptr M00_L04
M00_L14:
       call      qword ptr [7FFC54744258]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M00_L15:
       lea       rcx,[rsp+48]
       lea       rdx,[rsp+38]
       call      qword ptr [7FFC547442D0]
       jmp       near ptr M00_L04
M00_L16:
       lea       r8,[rsp+38]
       lea       rdx,[rsp+48]
       mov       rax,[rcx]
       mov       rax,[rax+48]
       call      qword ptr [rax+8]
       jmp       near ptr M00_L05
M00_L17:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 725
```