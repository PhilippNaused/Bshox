## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: DefaultJob)

```asm
; Benchmark.WriteString.Unicode()
;         var writer = new BshoxWriter(fixedBufferWriter);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         for (int i = 0; i < Count; i++)
;              ^^^^^^^^^
;             writer.WriteString(_stringsUnicode[i]);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         fixedBufferWriter.Reset();
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^
;         return writer.UnflushedBytes;
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       push      rbp
       push      r15
       push      r14
       push      r13
       push      r12
       push      rdi
       push      rsi
       push      rbx
       sub       rsp,0E8
       lea       rbp,[rsp+120]
       vxorps    xmm4,xmm4,xmm4
       vmovdqu   ymmword ptr [rbp-0E0],ymm4
       vmovdqu   ymmword ptr [rbp-0C0],ymm4
       vmovdqu   ymmword ptr [rbp-0A0],ymm4
       vmovdqu   ymmword ptr [rbp-80],ymm4
       vmovdqu   ymmword ptr [rbp-60],ymm4
       xor       eax,eax
       mov       [rbp-40],rax
       mov       rsi,rcx
       mov       rcx,[rsi+18]
       mov       [rbp-60],rcx
       mov       rcx,24D590012F0
       mov       rcx,[rcx]
       mov       [rbp-58],rcx
       xor       edi,edi
M00_L00:
       mov       rcx,[rsi+8]
       cmp       edi,[rcx+8]
       jae       near ptr M00_L32
       mov       rbx,[rcx+rdi*8+10]
       mov       r14d,[rbx+8]
       test      r14d,r14d
       je        near ptr M00_L12
       cmp       r14d,2A
       jg        near ptr M00_L22
       cmp       dword ptr [rbp-48],80
       jl        near ptr M00_L04
M00_L01:
       mov       r15,[rbp-50]
       add       rbx,0C
       xor       ecx,ecx
       mov       [rbp-0A8],rcx
       mov       [rbp-0B0],rcx
       mov       [rbp-0A8],rbx
       lea       rcx,[r15+1]
       mov       [rbp-0B0],rcx
       mov       r13,[rbp-0B0]
       test      rbx,rbx
       je        near ptr M00_L10
       test      r13,r13
       je        near ptr M00_L28
       mov       ecx,r14d
       or        ecx,7F
       jl        near ptr M00_L29
       mov       rcx,24D590012F8
       mov       r12,[rcx]
       lea       rcx,[rbp-0B8]
       mov       [rsp+20],rcx
       lea       rcx,[rbp-0C0]
       mov       [rsp+28],rcx
       mov       rcx,rbx
       mov       edx,r14d
       mov       r8,r13
       mov       r9d,7F
       call      qword ptr [7FFC54367C00]; System.Text.Unicode.Utf8Utility.TranscodeToUtf8(Char*, Int32, Byte*, Int32, Char* ByRef, Byte* ByRef)
       mov       rcx,[rbp-0B8]
       sub       rcx,rbx
       mov       rdx,rcx
       shr       rdx,3F
       add       rcx,rdx
       sar       rcx,1
       mov       eax,[rbp-0C0]
       sub       eax,r13d
       cmp       ecx,r14d
       jne       near ptr M00_L21
M00_L02:
       xor       ecx,ecx
       mov       [rbp-0A8],rcx
       mov       [rbp-0B0],rcx
       mov       [r15],al
       lea       ebx,[rax+1]
       test      ebx,ebx
       jl        near ptr M00_L30
       mov       ecx,ebx
       add       rcx,[rbp-50]
       mov       [rbp-50],rcx
       mov       ecx,[rbp-48]
       sub       ecx,ebx
       mov       [rbp-48],ecx
       add       ebx,[rbp-44]
       mov       [rbp-44],ebx
M00_L03:
       inc       edi
       cmp       edi,3E8
       jl        near ptr M00_L00
       mov       rdi,[rsi+18]
       lea       rsi,[rdi+18]
       add       rdi,8
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,[rbp-44]
       add       rsp,0E8
       pop       rbx
       pop       rsi
       pop       rdi
       pop       r12
       pop       r13
       pop       r14
       pop       r15
       pop       rbp
       ret
M00_L04:
       cmp       dword ptr [rbp-44],0
       jg        near ptr M00_L17
M00_L05:
       mov       rcx,[rbp-60]
       mov       rdx,[rbp-58]
       mov       r8d,[rdx+0C]
       cmp       r8d,80
       jle       near ptr M00_L18
M00_L06:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rcx],rdx
       jne       near ptr M00_L20
       lea       rdx,[rbp-90]
       call      qword ptr [7FFC545C6160]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       r15d,r15d
       xor       r13d,r13d
       mov       rcx,[rbp-90]
       test      rcx,rcx
       je        short M00_L08
       mov       rdx,[rcx]
       test      dword ptr [rdx],80000000
       je        near ptr M00_L19
       lea       r15,[rcx+10]
       mov       r13d,[rcx+8]
M00_L07:
       mov       edx,[rbp-88]
       and       edx,7FFFFFFF
       mov       ecx,[rbp-84]
       mov       r8d,ecx
       add       r8,rdx
       mov       r11d,r13d
       cmp       r8,r11
       ja        near ptr M00_L27
       add       r15,rdx
       mov       r13d,ecx
M00_L08:
       mov       [rbp-80],r15
       mov       [rbp-78],r13d
M00_L09:
       cmp       dword ptr [rbp-78],0
       jbe       near ptr M00_L32
       mov       rcx,[rbp-80]
       mov       [rbp-50],rcx
       mov       ecx,[rbp-78]
       mov       [rbp-48],ecx
       jmp       near ptr M00_L01
M00_L10:
       mov       ecx,0E
M00_L11:
       mov       edx,31
       call      qword ptr [7FFC54744168]
       int       3
M00_L12:
       cmp       dword ptr [rbp-48],0
       jg        short M00_L16
       cmp       dword ptr [rbp-44],0
       jle       short M00_L13
       mov       rcx,[rbp-60]
       mov       edx,[rbp-44]
       mov       r11,7FFC542B0558
       call      qword ptr [r11]
       xor       edx,edx
       mov       [rbp-50],rdx
       mov       [rbp-48],edx
       mov       [rbp-44],edx
M00_L13:
       mov       rcx,[rbp-60]
       mov       rdx,[rbp-58]
       mov       r8d,[rdx+0C]
       cmp       r8d,1
       jle       short M00_L14
       jmp       short M00_L15
M00_L14:
       mov       r8d,1
M00_L15:
       lea       rdx,[rbp-70]
       mov       r11,7FFC542B0550
       call      qword ptr [r11]
       cmp       dword ptr [rbp-68],0
       jbe       near ptr M00_L32
       mov       rax,[rbp-70]
       mov       [rbp-50],rax
       mov       eax,[rbp-68]
       mov       [rbp-48],eax
M00_L16:
       mov       rax,[rbp-50]
       mov       byte ptr [rax],0
       mov       rax,[rbp-50]
       inc       rax
       mov       [rbp-50],rax
       mov       eax,[rbp-48]
       dec       eax
       mov       [rbp-48],eax
       mov       eax,[rbp-44]
       inc       eax
       mov       [rbp-44],eax
       jmp       near ptr M00_L03
M00_L17:
       mov       rcx,[rbp-60]
       mov       edx,[rbp-44]
       mov       r11,7FFC542B0568
       call      qword ptr [r11]
       xor       edx,edx
       mov       [rbp-50],rdx
       mov       [rbp-48],edx
       mov       [rbp-44],edx
       jmp       near ptr M00_L05
M00_L18:
       mov       r8d,80
       jmp       near ptr M00_L06
M00_L19:
       lea       rdx,[rbp-0A0]
       mov       rax,[rcx]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       r15,[rbp-0A0]
       mov       r13d,[rbp-98]
       jmp       near ptr M00_L07
M00_L20:
       lea       rdx,[rbp-80]
       mov       r11,7FFC542B0560
       call      qword ptr [r11]
       jmp       near ptr M00_L09
M00_L21:
       mov       dword ptr [rsp+20],7F
       mov       [rsp+28],ecx
       mov       [rsp+30],eax
       mov       dword ptr [rsp+38],1
       mov       rcx,r12
       mov       rdx,rbx
       mov       r8d,r14d
       mov       r9,r13
       call      qword ptr [7FFC54744138]
       jmp       near ptr M00_L02
M00_L22:
       mov       rcx,24D590012F8
       mov       rcx,[rcx]
       mov       rdx,rbx
       call      qword ptr [7FFC5446CC60]; Precode of System.Text.UTF8Encoding.GetByteCount(System.String)
       mov       r15d,eax
       lea       rcx,[rbp-60]
       mov       edx,r15d
       call      qword ptr [7FFC546CFE28]
       cmp       [rbp-48],r15d
       jge       short M00_L26
       cmp       dword ptr [rbp-44],0
       jle       short M00_L23
       mov       rcx,[rbp-60]
       mov       edx,[rbp-44]
       mov       r11,7FFC542B0578
       call      qword ptr [r11]
       xor       edx,edx
       mov       [rbp-50],rdx
       mov       [rbp-48],edx
       mov       [rbp-44],edx
M00_L23:
       mov       rcx,[rbp-60]
       mov       rdx,[rbp-58]
       mov       r8d,[rdx+0C]
       cmp       r15d,r8d
       jge       short M00_L24
       jmp       short M00_L25
M00_L24:
       mov       r8d,r15d
M00_L25:
       lea       rdx,[rbp-0D0]
       mov       r11,7FFC542B0570
       call      qword ptr [r11]
       cmp       dword ptr [rbp-0C8],0
       jbe       near ptr M00_L32
       mov       rcx,[rbp-0D0]
       mov       [rbp-50],rcx
       mov       ecx,[rbp-0C8]
       mov       [rbp-48],ecx
M00_L26:
       mov       rdx,[rbp-50]
       lea       rcx,[rbx+0C]
       mov       r8d,r14d
       mov       [rbp-0E0],rcx
       mov       [rbp-0D8],r8d
       lea       rcx,[rbp-0E0]
       mov       r8d,r15d
       call      qword ptr [7FFC546CFDF8]; Bshox.Internals.EncodingHelper.Utf8Encode(System.ReadOnlySpan`1<Char>, Byte ByRef, Int32)
       test      r15d,r15d
       jl        short M00_L31
       mov       ecx,r15d
       add       rcx,[rbp-50]
       mov       [rbp-50],rcx
       mov       ecx,[rbp-48]
       sub       ecx,r15d
       mov       [rbp-48],ecx
       add       r15d,[rbp-44]
       mov       [rbp-44],r15d
       jmp       near ptr M00_L03
M00_L27:
       call      qword ptr [7FFC54537990]
       int       3
M00_L28:
       mov       ecx,0A
       jmp       near ptr M00_L11
M00_L29:
       mov       ecx,0C
       mov       edx,0D
       call      qword ptr [7FFC546C5770]
       int       3
M00_L30:
       mov       ecx,225
       mov       rdx,7FFC547242F8
       call      qword ptr [7FFC5436EE38]
       mov       rdx,rax
       mov       ecx,ebx
       call      qword ptr [7FFC54744108]
       int       3
M00_L31:
       mov       ecx,225
       mov       rdx,7FFC547242F8
       call      qword ptr [7FFC5436EE38]
       mov       rdx,rax
       mov       ecx,r15d
       call      qword ptr [7FFC54744108]
       int       3
M00_L32:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 1293
```
```asm
; System.Text.Unicode.Utf8Utility.TranscodeToUtf8(Char*, Int32, Byte*, Int32, Char* ByRef, Byte* ByRef)
       push      rsi
       push      rbx
       cmp       edx,r9d
       mov       eax,r9d
       cmovle    eax,edx
       xor       r10d,r10d
       cmp       rax,20
       jae       near ptr M01_L22
M01_L00:
       sub       rax,r10
       cmp       rax,4
       jb        near ptr M01_L37
       lea       r11,[r10+rax-4]
M01_L01:
       mov       rbx,[rcx+r10*2]
       mov       rsi,0FF80FF80FF80FF80
       test      rsi,rbx
       je        near ptr M01_L36
M01_L02:
       mov       eax,ebx
       test      eax,0FF80FF80
       je        near ptr M01_L40
M01_L03:
       test      eax,0FF80
       je        near ptr M01_L41
M01_L04:
       lea       rcx,[rcx+r10*2]
       add       r8,r10
       cmp       r10d,edx
       je        near ptr M01_L42
       sub       edx,r10d
       sub       r9d,r10d
       cmp       edx,2
       jl        near ptr M01_L13
       mov       eax,edx
       lea       rax,[rcx+rax*2-4]
M01_L05:
       mov       r10d,[rcx]
M01_L06:
       test      r10d,0FF80FF80
       je        near ptr M01_L43
M01_L07:
       test      r10d,0FF80
       jne       short M01_L08
       test      r9d,r9d
       je        near ptr M01_L62
       mov       [r8],r10b
       add       rcx,2
       inc       r8
       dec       r9d
       cmp       rcx,rax
       ja        near ptr M01_L12
       mov       r10d,[rcx]
M01_L08:
       test      r10d,0F800
       jne       near ptr M01_L17
       lea       r11d,[r10-800000]
       cmp       r11d,77FFFFF
       ja        short M01_L10
M01_L09:
       cmp       r9d,4
       jl        near ptr M01_L59
       mov       r11d,r10d
       shr       r11d,6
       and       r11d,1F001F
       shl       r10d,8
       and       r10d,3F003F00
       add       r10d,r11d
       add       r10d,80C080C0
       mov       [r8],r10d
       add       rcx,4
       add       r8,4
       add       r9d,0FFFFFFFC
       cmp       rcx,rax
       ja        near ptr M01_L12
       mov       r10d,[rcx]
       lea       r11d,[r10-80]
       movzx     r11d,r11w
       cmp       r11d,780
       jge       near ptr M01_L06
       lea       r11d,[r10-800000]
       cmp       r11d,77FFFFF
       jbe       short M01_L09
M01_L10:
       cmp       r9d,2
       jl        near ptr M01_L62
       lea       r11d,[r10*4]
       and       r11d,1F00
       mov       ebx,r10d
       and       ebx,3F
       lea       r11d,[r11+rbx+0C080]
       movbe     [r8],r11w
       cmp       r10d,800000
       jae       near ptr M01_L50
       cmp       r9d,3
       jl        near ptr M01_L58
       shr       r10d,10
       mov       [r8+2],r10b
       add       rcx,4
       add       r8,3
       add       r9d,0FFFFFFFD
M01_L11:
       cmp       rcx,rax
       jbe       near ptr M01_L05
M01_L12:
       sub       rax,rcx
       mov       rdx,rax
       shr       rdx,3F
       add       rdx,rax
       sar       rdx,1
       add       edx,2
M01_L13:
       test      edx,edx
       jne       near ptr M01_L18
M01_L14:
       xor       eax,eax
M01_L15:
       mov       rdx,[rsp+38]
       mov       [rdx],rcx
       mov       r10,[rsp+40]
       mov       [r10],r8
       vzeroupper
       pop       rbx
       pop       rsi
       ret
M01_L16:
       cmp       r9d,3
       jl        near ptr M01_L62
       lea       r11d,[r10*4]
       and       r11d,3F00
       movzx     ebx,r10w
       shr       ebx,0C
       add       r11d,ebx
       add       r11d,80E0
       mov       [r8],r11w
       mov       r11d,r10d
       and       r11d,3F
       or        r11d,0FFFFFF80
       mov       [r8+2],r11b
       add       rcx,2
       add       r8,3
       add       r9d,0FFFFFFFD
       cmp       r10d,800000
       jae       near ptr M01_L52
       test      r9d,r9d
       je        near ptr M01_L62
       shr       r10d,10
       mov       [r8],r10b
       add       rcx,2
       inc       r8
       dec       r9d
       cmp       rcx,rax
       ja        near ptr M01_L12
       mov       r10d,[rcx]
       test      r10d,0F800
       je        near ptr M01_L06
M01_L17:
       lea       r11d,[r10-0D800]
       test      r11d,0F800
       je        near ptr M01_L54
       test      r10d,0F8000000
       je        near ptr M01_L16
       lea       r11d,[r10+28000000]
       cmp       r11d,8000000
       jb        near ptr M01_L16
       cmp       r9d,6
       jl        near ptr M01_L16
       lea       r11d,[r10*4]
       and       r11d,3F00
       mov       ebx,r10d
       and       ebx,3F
       shl       ebx,10
       or        r11d,ebx
       mov       ebx,r10d
       shr       ebx,4
       and       ebx,0F000000
       mov       esi,r10d
       shr       esi,0C
       and       esi,0F
       or        ebx,esi
       add       r11d,ebx
       add       r11d,0E08080E0
       mov       [r8],r11d
       mov       r11d,r10d
       shr       r11d,16
       and       r11d,3F
       shr       r10d,8
       and       r10d,3F00
       add       r10d,r11d
       add       r10d,8080
       mov       [r8+4],r10w
       add       rcx,4
       add       r8,6
       add       r9d,0FFFFFFFA
       cmp       rcx,rax
       ja        near ptr M01_L12
       mov       r10d,[rcx]
       test      r10d,0F800
       jne       near ptr M01_L17
       jmp       near ptr M01_L06
M01_L18:
       movzx     eax,word ptr [rcx]
M01_L19:
       cmp       eax,7F
       jbe       near ptr M01_L60
       cmp       eax,800
       jb        short M01_L21
       lea       r10d,[rax-0D800]
       cmp       r10d,7FF
       jbe       near ptr M01_L61
       cmp       r9d,3
       jl        near ptr M01_L62
       mov       r9d,eax
       and       r9d,3F
       or        r9d,0FFFFFF80
       mov       [r8+2],r9b
       mov       r10d,eax
       shr       r10d,6
       and       r10d,3F
       or        r10d,0FFFFFF80
       mov       [r8+1],r10b
       shr       eax,0C
       or        eax,0FFFFFFE0
       mov       [r8],al
       add       rcx,2
       add       r8,3
M01_L20:
       cmp       edx,1
       jg        near ptr M01_L62
       jmp       near ptr M01_L14
M01_L21:
       cmp       r9d,2
       jl        near ptr M01_L62
       mov       r9d,eax
       and       r9d,3F
       or        r9d,0FFFFFF80
       mov       [r8+1],r9b
       shr       eax,6
       or        eax,0FFFFFFC0
       mov       [r8],al
       add       rcx,2
       add       r8,2
       jmp       short M01_L20
M01_L22:
       mov       r11,[rcx]
       mov       rbx,0FF80FF80FF80FF80
       test      rbx,r11
       mov       rbx,r11
       jne       near ptr M01_L02
       cmp       rax,40
       jb        near ptr M01_L29
       mov       r10,rcx
       vmovups   ymm0,[r10]
       vptest    ymm0,ymmword ptr [7FFC543DD640]
       je        short M01_L23
       xor       r10d,r10d
       jmp       near ptr M01_L28
M01_L23:
       mov       r11,r8
       vpackuswb ymm0,ymm0,ymm0
       vpermq    ymm0,ymm0,0D8
       vmovups   [r11],xmm0
       mov       ebx,10
       test      r8b,10
       jne       short M01_L24
       vmovups   ymm0,[r10+20]
       vptest    ymm0,ymmword ptr [7FFC543DD640]
       jne       short M01_L26
       vpackuswb ymm0,ymm0,ymm0
       vpermq    ymm0,ymm0,0D8
       vmovups   [r11+10],xmm0
M01_L24:
       mov       rbx,r8
       and       rbx,1F
       neg       rbx
       add       rbx,20
       lea       rsi,[rax-20]
M01_L25:
       vmovups   ymm0,[r10+rbx*2]
       vmovups   ymm1,[r10+rbx*2+20]
       vpor      ymm2,ymm0,ymm1
       vptest    ymm2,ymmword ptr [7FFC543DD640]
       jne       short M01_L27
       vpackuswb ymm0,ymm0,ymm1
       vpermq    ymm0,ymm0,0D8
       vmovups   [r11+rbx],ymm0
       add       rbx,20
       cmp       rbx,rsi
       jbe       short M01_L25
M01_L26:
       mov       r10,rbx
       jmp       short M01_L28
M01_L27:
       vptest    ymm0,ymmword ptr [7FFC543DD640]
       jne       short M01_L26
       vpackuswb ymm0,ymm0,ymm0
       vpermq    ymm0,ymm0,0D8
       vmovups   [r11+rbx],xmm0
       add       rbx,10
       jmp       short M01_L26
M01_L28:
       jmp       near ptr M01_L00
M01_L29:
       mov       r10,rcx
       vmovups   xmm0,[r10]
       vptest    xmm0,xmmword ptr [7FFC543DD640]
       je        short M01_L30
       xor       r10d,r10d
       jmp       near ptr M01_L35
M01_L30:
       mov       r11,r8
       vpackuswb xmm0,xmm0,xmm0
       vmovsd    qword ptr [r11],xmm0
       mov       ebx,8
       test      r8b,8
       jne       short M01_L31
       vmovups   xmm0,[r10+10]
       vptest    xmm0,xmmword ptr [7FFC543DD640]
       jne       short M01_L33
       vpackuswb xmm0,xmm0,xmm0
       vmovsd    qword ptr [r11+8],xmm0
M01_L31:
       mov       rbx,r8
       and       rbx,0F
       neg       rbx
       add       rbx,10
       lea       rsi,[rax-10]
M01_L32:
       vmovups   xmm0,[r10+rbx*2]
       vmovups   xmm1,[r10+rbx*2+10]
       vpor      xmm2,xmm0,xmm1
       vptest    xmm2,xmmword ptr [7FFC543DD640]
       jne       short M01_L34
       vpackuswb xmm0,xmm0,xmm1
       vmovups   [r11+rbx],xmm0
       add       rbx,10
       cmp       rbx,rsi
       jbe       short M01_L32
M01_L33:
       mov       r10,rbx
       jmp       short M01_L35
M01_L34:
       vptest    xmm0,xmmword ptr [7FFC543DD640]
       jne       short M01_L33
       vpackuswb xmm0,xmm0,xmm0
       vmovsd    qword ptr [r11+rbx],xmm0
       add       rbx,8
       jmp       short M01_L33
M01_L35:
       jmp       near ptr M01_L00
M01_L36:
       vmovq     xmm0,rbx
       vpackuswb xmm0,xmm0,xmm0
       vmovd     dword ptr [r8+r10],xmm0
       add       r10,4
       cmp       r10,r11
       jbe       near ptr M01_L01
M01_L37:
       test      al,2
       je        short M01_L38
       mov       r11d,[rcx+r10*2]
       test      r11d,0FF80FF80
       jne       short M01_L39
       mov       rbx,rax
       mov       eax,r11d
       mov       r11,rbx
       lea       rbx,[r8+r10]
       mov       [rbx],al
       shr       eax,10
       mov       [rbx+1],al
       add       r10,2
       mov       rax,r11
M01_L38:
       test      al,1
       je        near ptr M01_L04
       movzx     eax,word ptr [rcx+r10*2]
       cmp       eax,7F
       ja        near ptr M01_L04
       jmp       short M01_L41
M01_L39:
       mov       eax,r11d
       jmp       near ptr M01_L03
M01_L40:
       lea       r11,[r8+r10]
       mov       [r11],al
       shr       eax,10
       mov       [r11+1],al
       shr       rbx,20
       mov       eax,ebx
       add       r10,2
       jmp       near ptr M01_L03
M01_L41:
       mov       [r8+r10],al
       inc       r10
       jmp       near ptr M01_L04
M01_L42:
       mov       rdx,[rsp+38]
       mov       [rdx],rcx
       mov       r10,[rsp+40]
       mov       [r10],r8
       xor       eax,eax
       vzeroupper
       pop       rbx
       pop       rsi
       ret
M01_L43:
       cmp       r9d,2
       jl        near ptr M01_L59
       mov       r11d,r10d
       shr       r11d,8
       or        r11d,r10d
       mov       [r8],r11w
       add       rcx,4
       add       r8,2
       add       r9d,0FFFFFFFE
       mov       r10,rax
       sub       r10,rcx
       mov       r11,r10
       shr       r11,3F
       add       r10,r11
       sar       r10,1
       add       r10d,2
       movsxd    r11,r9d
       cmp       r10,r11
       jle       short M01_L44
       jmp       short M01_L45
M01_L44:
       mov       r11,r10
M01_L45:
       mov       r10d,r11d
       shr       r10d,3
       xor       ebx,ebx
       jmp       short M01_L47
M01_L46:
       vmovups   xmm0,[rcx]
       vptest    xmm0,xmmword ptr [7FFC543DD640]
       jne       short M01_L48
       vpackuswb xmm0,xmm0,xmm0
       vmovq     qword ptr [r8],xmm0
       add       rcx,10
       add       r8,8
       inc       ebx
M01_L47:
       cmp       ebx,r10d
       jb        short M01_L46
       lea       r10d,[rbx*8]
       sub       r9d,r10d
       test      r11b,4
       je        near ptr M01_L11
       mov       r10,[rcx]
       mov       r11,0FF80FF80FF80FF80
       test      r11,r10
       jne       short M01_L49
       jmp       near ptr M01_L53
M01_L48:
       lea       r11d,[rbx*8]
       sub       r9d,r11d
       vmovq     r10,xmm0
       mov       r11,0FF80FF80FF80FF80
       test      r11,r10
       jne       short M01_L49
       vpackuswb xmm1,xmm0,xmm0
       vmovd     dword ptr [r8],xmm1
       add       rcx,8
       add       r8,4
       add       r9d,0FFFFFFFC
       vpextrq   r10,xmm0,1
M01_L49:
       mov       r11d,r10d
       test      r11d,0FF80FF80
       jne       short M01_L51
       mov       ebx,r11d
       shr       ebx,8
       or        ebx,r11d
       mov       [r8],bx
       add       rcx,4
       add       r8,2
       add       r9d,0FFFFFFFE
       shr       r10,20
       mov       r11d,r10d
       mov       r10d,r11d
       jmp       near ptr M01_L07
M01_L50:
       add       rcx,2
       add       r8,2
       add       r9d,0FFFFFFFE
       cmp       rcx,rax
       ja        near ptr M01_L12
       mov       r10d,[rcx]
       jmp       near ptr M01_L17
M01_L51:
       mov       r10d,r11d
       jmp       near ptr M01_L07
M01_L52:
       cmp       rcx,rax
       ja        near ptr M01_L12
       mov       r10d,[rcx]
       jmp       near ptr M01_L07
M01_L53:
       vmovq     xmm0,r10
       vpackuswb xmm0,xmm0,xmm0
       vmovd     dword ptr [r8],xmm0
       add       rcx,8
       jmp       short M01_L57
M01_L54:
       lea       r11d,[r10+23FF2800]
       test      r11d,0FC00FC00
       jne       short M01_L55
       cmp       r9d,4
       jl        near ptr M01_L62
       jmp       short M01_L56
M01_L55:
       mov       eax,3
       jmp       near ptr M01_L15
M01_L56:
       add       r10d,40
       mov       r11d,r10d
       and       r11d,3
       shl       r11d,14
       or        r11d,808080F0
       mov       ebx,r10d
       and       ebx,3F0700
       bswap     ebx
       rol       ebx,10
       or        r11d,ebx
       mov       ebx,r10d
       shr       ebx,6
       and       ebx,0F0000
       or        r11d,ebx
       and       r10d,0FC
       shl       r10d,6
       or        r10d,r11d
       mov       [r8],r10d
       add       rcx,4
M01_L57:
       add       r8,4
       add       r9d,0FFFFFFFC
       jmp       near ptr M01_L11
M01_L58:
       add       rcx,2
       add       r8,2
       jmp       short M01_L62
M01_L59:
       movzx     eax,r10w
       jmp       near ptr M01_L19
M01_L60:
       test      r9d,r9d
       je        short M01_L62
       mov       [r8],al
       add       rcx,2
       inc       r8
       jmp       near ptr M01_L20
M01_L61:
       cmp       eax,0DBFF
       ja        near ptr M01_L55
       mov       eax,2
       jmp       near ptr M01_L15
M01_L62:
       mov       eax,1
       jmp       near ptr M01_L15
; Total bytes of code 2105
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
       call      qword ptr [7FFC546CFF00]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 48
```
```asm
; Bshox.Internals.EncodingHelper.Utf8Encode(System.ReadOnlySpan`1<Char>, Byte ByRef, Int32)
;     {
;     ^
;         fixed (char* charsPtr = &MemoryMarshal.GetReference(chars))
;                ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         fixed (byte* bytesPtr = &bytes)
;                ^^^^^^^^^^^^^^^^^^^^^^^
;             return Utf8NoBom.GetBytes(charsPtr, chars.Length, bytesPtr, byteCount);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       sub       rsp,38
       mov       r9,[rcx]
       mov       [rsp+30],r9
       mov       rax,r9
       mov       [rsp+28],rdx
       mov       r9d,[rcx+8]
       mov       [rsp+20],r8d
       mov       r8d,r9d
       mov       r9,rdx
       mov       rcx,24D590012F8
       mov       rcx,[rcx]
       mov       rdx,rax
       call      qword ptr [7FFC5446CCA8]; System.Text.UTF8Encoding.GetBytes(Char*, Int32, Byte*, Int32)
       nop
       add       rsp,38
       ret
; Total bytes of code 63
```

## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: DefaultJob)

```asm
; Benchmark.WriteString.Ascii()
;         var writer = new BshoxWriter(fixedBufferWriter);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         for (int i = 0; i < Count; i++)
;              ^^^^^^^^^
;             writer.WriteString(_stringsAscii[i]);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         fixedBufferWriter.Reset();
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^
;         return writer.UnflushedBytes;
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       push      rbp
       push      r15
       push      r14
       push      r13
       push      r12
       push      rdi
       push      rsi
       push      rbx
       sub       rsp,0E8
       lea       rbp,[rsp+120]
       vxorps    xmm4,xmm4,xmm4
       vmovdqu   ymmword ptr [rbp-0E0],ymm4
       vmovdqu   ymmword ptr [rbp-0C0],ymm4
       vmovdqu   ymmword ptr [rbp-0A0],ymm4
       vmovdqu   ymmword ptr [rbp-80],ymm4
       vmovdqu   ymmword ptr [rbp-60],ymm4
       xor       eax,eax
       mov       [rbp-40],rax
       mov       rsi,rcx
       mov       rcx,[rsi+18]
       mov       [rbp-60],rcx
       mov       rcx,13177C012F0
       mov       rcx,[rcx]
       mov       [rbp-58],rcx
       xor       edi,edi
M00_L00:
       mov       rcx,[rsi+10]
       cmp       edi,[rcx+8]
       jae       near ptr M00_L32
       mov       rbx,[rcx+rdi*8+10]
       mov       r14d,[rbx+8]
       test      r14d,r14d
       je        near ptr M00_L12
       cmp       r14d,2A
       jg        near ptr M00_L22
       cmp       dword ptr [rbp-48],80
       jl        near ptr M00_L04
M00_L01:
       mov       r15,[rbp-50]
       add       rbx,0C
       xor       ecx,ecx
       mov       [rbp-0A8],rcx
       mov       [rbp-0B0],rcx
       mov       [rbp-0A8],rbx
       lea       rcx,[r15+1]
       mov       [rbp-0B0],rcx
       mov       r13,[rbp-0B0]
       test      rbx,rbx
       je        near ptr M00_L10
       test      r13,r13
       je        near ptr M00_L28
       mov       ecx,r14d
       or        ecx,7F
       jl        near ptr M00_L29
       mov       rcx,13177C012F8
       mov       r12,[rcx]
       lea       rcx,[rbp-0B8]
       mov       [rsp+20],rcx
       lea       rcx,[rbp-0C0]
       mov       [rsp+28],rcx
       mov       rcx,rbx
       mov       edx,r14d
       mov       r8,r13
       mov       r9d,7F
       call      qword ptr [7FFC54357C00]; System.Text.Unicode.Utf8Utility.TranscodeToUtf8(Char*, Int32, Byte*, Int32, Char* ByRef, Byte* ByRef)
       mov       rcx,[rbp-0B8]
       sub       rcx,rbx
       mov       rdx,rcx
       shr       rdx,3F
       add       rcx,rdx
       sar       rcx,1
       mov       eax,[rbp-0C0]
       sub       eax,r13d
       cmp       ecx,r14d
       jne       near ptr M00_L21
M00_L02:
       xor       ecx,ecx
       mov       [rbp-0A8],rcx
       mov       [rbp-0B0],rcx
       mov       [r15],al
       lea       ebx,[rax+1]
       test      ebx,ebx
       jl        near ptr M00_L30
       mov       ecx,ebx
       add       rcx,[rbp-50]
       mov       [rbp-50],rcx
       mov       ecx,[rbp-48]
       sub       ecx,ebx
       mov       [rbp-48],ecx
       add       ebx,[rbp-44]
       mov       [rbp-44],ebx
M00_L03:
       inc       edi
       cmp       edi,3E8
       jl        near ptr M00_L00
       mov       rdi,[rsi+18]
       lea       rsi,[rdi+18]
       add       rdi,8
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,[rbp-44]
       add       rsp,0E8
       pop       rbx
       pop       rsi
       pop       rdi
       pop       r12
       pop       r13
       pop       r14
       pop       r15
       pop       rbp
       ret
M00_L04:
       cmp       dword ptr [rbp-44],0
       jg        near ptr M00_L17
M00_L05:
       mov       rcx,[rbp-60]
       mov       rdx,[rbp-58]
       mov       r8d,[rdx+0C]
       cmp       r8d,80
       jle       near ptr M00_L18
M00_L06:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rcx],rdx
       jne       near ptr M00_L20
       lea       rdx,[rbp-90]
       call      qword ptr [7FFC545B6160]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       r15d,r15d
       xor       r13d,r13d
       mov       rcx,[rbp-90]
       test      rcx,rcx
       je        short M00_L08
       mov       rdx,[rcx]
       test      dword ptr [rdx],80000000
       je        near ptr M00_L19
       lea       r15,[rcx+10]
       mov       r13d,[rcx+8]
M00_L07:
       mov       edx,[rbp-88]
       and       edx,7FFFFFFF
       mov       ecx,[rbp-84]
       mov       r8d,ecx
       add       r8,rdx
       mov       r11d,r13d
       cmp       r8,r11
       ja        near ptr M00_L27
       add       r15,rdx
       mov       r13d,ecx
M00_L08:
       mov       [rbp-80],r15
       mov       [rbp-78],r13d
M00_L09:
       cmp       dword ptr [rbp-78],0
       jbe       near ptr M00_L32
       mov       rcx,[rbp-80]
       mov       [rbp-50],rcx
       mov       ecx,[rbp-78]
       mov       [rbp-48],ecx
       jmp       near ptr M00_L01
M00_L10:
       mov       ecx,0E
M00_L11:
       mov       edx,31
       call      qword ptr [7FFC54734168]
       int       3
M00_L12:
       cmp       dword ptr [rbp-48],0
       jg        short M00_L16
       cmp       dword ptr [rbp-44],0
       jle       short M00_L13
       mov       rcx,[rbp-60]
       mov       edx,[rbp-44]
       mov       r11,7FFC542A0558
       call      qword ptr [r11]
       xor       edx,edx
       mov       [rbp-50],rdx
       mov       [rbp-48],edx
       mov       [rbp-44],edx
M00_L13:
       mov       rcx,[rbp-60]
       mov       rdx,[rbp-58]
       mov       r8d,[rdx+0C]
       cmp       r8d,1
       jle       short M00_L14
       jmp       short M00_L15
M00_L14:
       mov       r8d,1
M00_L15:
       lea       rdx,[rbp-70]
       mov       r11,7FFC542A0550
       call      qword ptr [r11]
       cmp       dword ptr [rbp-68],0
       jbe       near ptr M00_L32
       mov       rax,[rbp-70]
       mov       [rbp-50],rax
       mov       eax,[rbp-68]
       mov       [rbp-48],eax
M00_L16:
       mov       rax,[rbp-50]
       mov       byte ptr [rax],0
       mov       rax,[rbp-50]
       inc       rax
       mov       [rbp-50],rax
       mov       eax,[rbp-48]
       dec       eax
       mov       [rbp-48],eax
       mov       eax,[rbp-44]
       inc       eax
       mov       [rbp-44],eax
       jmp       near ptr M00_L03
M00_L17:
       mov       rcx,[rbp-60]
       mov       edx,[rbp-44]
       mov       r11,7FFC542A0568
       call      qword ptr [r11]
       xor       edx,edx
       mov       [rbp-50],rdx
       mov       [rbp-48],edx
       mov       [rbp-44],edx
       jmp       near ptr M00_L05
M00_L18:
       mov       r8d,80
       jmp       near ptr M00_L06
M00_L19:
       lea       rdx,[rbp-0A0]
       mov       rax,[rcx]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       r15,[rbp-0A0]
       mov       r13d,[rbp-98]
       jmp       near ptr M00_L07
M00_L20:
       lea       rdx,[rbp-80]
       mov       r11,7FFC542A0560
       call      qword ptr [r11]
       jmp       near ptr M00_L09
M00_L21:
       mov       dword ptr [rsp+20],7F
       mov       [rsp+28],ecx
       mov       [rsp+30],eax
       mov       dword ptr [rsp+38],1
       mov       rcx,r12
       mov       rdx,rbx
       mov       r8d,r14d
       mov       r9,r13
       call      qword ptr [7FFC54734138]
       jmp       near ptr M00_L02
M00_L22:
       mov       rcx,13177C012F8
       mov       rcx,[rcx]
       mov       rdx,rbx
       call      qword ptr [7FFC5445CC60]; Precode of System.Text.UTF8Encoding.GetByteCount(System.String)
       mov       r15d,eax
       lea       rcx,[rbp-60]
       mov       edx,r15d
       call      qword ptr [7FFC546BFE28]
       cmp       [rbp-48],r15d
       jge       short M00_L26
       cmp       dword ptr [rbp-44],0
       jle       short M00_L23
       mov       rcx,[rbp-60]
       mov       edx,[rbp-44]
       mov       r11,7FFC542A0578
       call      qword ptr [r11]
       xor       edx,edx
       mov       [rbp-50],rdx
       mov       [rbp-48],edx
       mov       [rbp-44],edx
M00_L23:
       mov       rcx,[rbp-60]
       mov       rdx,[rbp-58]
       mov       r8d,[rdx+0C]
       cmp       r15d,r8d
       jge       short M00_L24
       jmp       short M00_L25
M00_L24:
       mov       r8d,r15d
M00_L25:
       lea       rdx,[rbp-0D0]
       mov       r11,7FFC542A0570
       call      qword ptr [r11]
       cmp       dword ptr [rbp-0C8],0
       jbe       near ptr M00_L32
       mov       rcx,[rbp-0D0]
       mov       [rbp-50],rcx
       mov       ecx,[rbp-0C8]
       mov       [rbp-48],ecx
M00_L26:
       mov       rdx,[rbp-50]
       lea       rcx,[rbx+0C]
       mov       r8d,r14d
       mov       [rbp-0E0],rcx
       mov       [rbp-0D8],r8d
       lea       rcx,[rbp-0E0]
       mov       r8d,r15d
       call      qword ptr [7FFC546BFDF8]; Bshox.Internals.EncodingHelper.Utf8Encode(System.ReadOnlySpan`1<Char>, Byte ByRef, Int32)
       test      r15d,r15d
       jl        short M00_L31
       mov       ecx,r15d
       add       rcx,[rbp-50]
       mov       [rbp-50],rcx
       mov       ecx,[rbp-48]
       sub       ecx,r15d
       mov       [rbp-48],ecx
       add       r15d,[rbp-44]
       mov       [rbp-44],r15d
       jmp       near ptr M00_L03
M00_L27:
       call      qword ptr [7FFC54527990]
       int       3
M00_L28:
       mov       ecx,0A
       jmp       near ptr M00_L11
M00_L29:
       mov       ecx,0C
       mov       edx,0D
       call      qword ptr [7FFC546B5770]
       int       3
M00_L30:
       mov       ecx,225
       mov       rdx,7FFC547142F8
       call      qword ptr [7FFC5435EE38]
       mov       rdx,rax
       mov       ecx,ebx
       call      qword ptr [7FFC54734108]
       int       3
M00_L31:
       mov       ecx,225
       mov       rdx,7FFC547142F8
       call      qword ptr [7FFC5435EE38]
       mov       rdx,rax
       mov       ecx,r15d
       call      qword ptr [7FFC54734108]
       int       3
M00_L32:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 1293
```
```asm
; System.Text.Unicode.Utf8Utility.TranscodeToUtf8(Char*, Int32, Byte*, Int32, Char* ByRef, Byte* ByRef)
       push      rdi
       push      rsi
       push      rbx
       cmp       edx,r9d
       mov       eax,r9d
       cmovle    eax,edx
       xor       r10d,r10d
       cmp       rax,20
       jae       near ptr M01_L05
M01_L00:
       sub       rax,r10
       cmp       rax,4
       jb        short M01_L02
       lea       r11,[r10+rax-4]
       mov       rbx,[rcx+r10*2]
       mov       rsi,0FF80FF80FF80FF80
       test      rsi,rbx
       jne       near ptr M01_L20
M01_L01:
       vmovq     xmm0,rbx
       vpackuswb xmm0,xmm0,xmm0
       vmovd     dword ptr [r8+r10],xmm0
       add       r10,4
       cmp       r10,r11
       ja        short M01_L02
       mov       rbx,[rcx+r10*2]
       mov       rsi,0FF80FF80FF80FF80
       test      rsi,rbx
       jne       near ptr M01_L20
       jmp       short M01_L01
M01_L02:
       test      al,2
       je        short M01_L03
       mov       r11d,[rcx+r10*2]
       test      r11d,0FF80FF80
       jne       near ptr M01_L21
       lea       rbx,[r8+r10]
       mov       [rbx],r11b
       shr       r11d,10
       mov       [rbx+1],r11b
       add       r10,2
M01_L03:
       test      al,1
       jne       near ptr M01_L19
M01_L04:
       lea       rcx,[rcx+r10*2]
       add       r8,r10
       cmp       r10d,edx
       jne       near ptr M01_L24
       mov       r11,[rsp+40]
       mov       [r11],rcx
       mov       rax,[rsp+48]
       mov       [rax],r8
       xor       eax,eax
       vzeroupper
       pop       rbx
       pop       rsi
       pop       rdi
       ret
M01_L05:
       mov       r11,[rcx]
       mov       rbx,0FF80FF80FF80FF80
       test      rbx,r11
       mov       rbx,r11
       jne       near ptr M01_L20
       cmp       rax,40
       jb        near ptr M01_L12
       mov       r10,rcx
       vmovups   ymm0,[r10]
       vptest    ymm0,ymmword ptr [7FFC543CD740]
       je        short M01_L06
       xor       r10d,r10d
       jmp       near ptr M01_L11
M01_L06:
       mov       r11,r8
       vpackuswb ymm0,ymm0,ymm0
       vpermq    ymm0,ymm0,0D8
       vmovups   [r11],xmm0
       mov       ebx,10
       test      r8b,10
       jne       short M01_L07
       vmovups   ymm0,[r10+20]
       vptest    ymm0,ymmword ptr [7FFC543CD740]
       jne       short M01_L09
       vpackuswb ymm0,ymm0,ymm0
       vpermq    ymm0,ymm0,0D8
       vmovups   [r11+10],xmm0
M01_L07:
       mov       rbx,r8
       and       rbx,1F
       neg       rbx
       add       rbx,20
       lea       rsi,[rax-20]
M01_L08:
       vmovups   ymm0,[r10+rbx*2]
       vmovups   ymm1,[r10+rbx*2+20]
       vpor      ymm2,ymm0,ymm1
       vptest    ymm2,ymmword ptr [7FFC543CD740]
       jne       short M01_L10
       vpackuswb ymm0,ymm0,ymm1
       vpermq    ymm0,ymm0,0D8
       vmovups   [r11+rbx],ymm0
       add       rbx,20
       cmp       rbx,rsi
       jbe       short M01_L08
M01_L09:
       mov       r10,rbx
       jmp       short M01_L11
M01_L10:
       vptest    ymm0,ymmword ptr [7FFC543CD740]
       jne       short M01_L09
       vpackuswb ymm0,ymm0,ymm0
       vpermq    ymm0,ymm0,0D8
       vmovups   [r11+rbx],xmm0
       add       rbx,10
       jmp       short M01_L09
M01_L11:
       jmp       near ptr M01_L00
M01_L12:
       mov       r10,rcx
       vmovups   xmm0,[r10]
       vptest    xmm0,xmmword ptr [7FFC543CD740]
       je        short M01_L13
       xor       r10d,r10d
       jmp       near ptr M01_L18
M01_L13:
       mov       r11,r8
       vpackuswb xmm0,xmm0,xmm0
       vmovsd    qword ptr [r11],xmm0
       mov       ebx,8
       test      r8b,8
       jne       short M01_L14
       vmovups   xmm0,[r10+10]
       vptest    xmm0,xmmword ptr [7FFC543CD740]
       jne       short M01_L16
       vpackuswb xmm0,xmm0,xmm0
       vmovsd    qword ptr [r11+8],xmm0
M01_L14:
       mov       rbx,r8
       and       rbx,0F
       neg       rbx
       add       rbx,10
       lea       rsi,[rax-10]
M01_L15:
       vmovups   xmm0,[r10+rbx*2]
       vmovups   xmm1,[r10+rbx*2+10]
       vpor      xmm2,xmm0,xmm1
       vptest    xmm2,xmmword ptr [7FFC543CD740]
       jne       short M01_L17
       vpackuswb xmm0,xmm0,xmm1
       vmovups   [r11+rbx],xmm0
       add       rbx,10
       cmp       rbx,rsi
       jbe       short M01_L15
M01_L16:
       mov       r10,rbx
       jmp       short M01_L18
M01_L17:
       vptest    xmm0,xmmword ptr [7FFC543CD740]
       jne       short M01_L16
       vpackuswb xmm0,xmm0,xmm0
       vmovsd    qword ptr [r11+rbx],xmm0
       add       rbx,8
       jmp       short M01_L16
M01_L18:
       jmp       near ptr M01_L00
M01_L19:
       movzx     r11d,word ptr [rcx+r10*2]
       cmp       r11d,7F
       ja        near ptr M01_L04
       jmp       short M01_L22
M01_L20:
       mov       eax,ebx
       test      eax,0FF80FF80
       jne       short M01_L23
       lea       r11,[r8+r10]
       mov       [r11],al
       shr       eax,10
       mov       [r11+1],al
       shr       rbx,20
       mov       eax,ebx
       add       r10,2
       mov       r11d,eax
M01_L21:
       test      r11d,0FF80
       jne       near ptr M01_L04
M01_L22:
       mov       [r8+r10],r11b
       inc       r10
       jmp       near ptr M01_L04
M01_L23:
       mov       r11d,eax
       jmp       short M01_L21
M01_L24:
       sub       edx,r10d
       sub       r9d,r10d
       cmp       edx,2
       jl        near ptr M01_L61
       mov       eax,edx
       lea       rax,[rcx+rax*2-4]
M01_L25:
       mov       r10d,[rcx]
       jmp       near ptr M01_L50
M01_L26:
       cmp       r9d,2
       jl        near ptr M01_L62
       mov       ebx,r10d
       shr       ebx,8
       or        ebx,r10d
       mov       [r8],bx
       add       rcx,4
       add       r8,2
       add       r9d,0FFFFFFFE
       mov       r10,rax
       sub       r10,rcx
       mov       rbx,r10
       shr       rbx,3F
       add       r10,rbx
       sar       r10,1
       add       r10d,2
       movsxd    rbx,r9d
       cmp       r10,rbx
       jle       short M01_L27
       jmp       short M01_L28
M01_L27:
       mov       rbx,r10
M01_L28:
       mov       r10d,ebx
       shr       r10d,3
       xor       esi,esi
       jmp       short M01_L30
M01_L29:
       vmovups   xmm0,[rcx]
       vptest    xmm0,xmmword ptr [7FFC543CD740]
       jne       short M01_L31
       vpackuswb xmm0,xmm0,xmm0
       vmovq     qword ptr [r8],xmm0
       add       rcx,10
       add       r8,8
       inc       esi
M01_L30:
       cmp       esi,r10d
       jb        short M01_L29
       lea       r10d,[rsi*8]
       sub       r9d,r10d
       test      bl,4
       je        near ptr M01_L58
       mov       r10,[rcx]
       mov       rbx,0FF80FF80FF80FF80
       test      rbx,r10
       jne       short M01_L32
       jmp       near ptr M01_L53
M01_L31:
       lea       r10d,[rsi*8]
       sub       r9d,r10d
       vmovq     r10,xmm0
       mov       rsi,0FF80FF80FF80FF80
       test      rsi,r10
       jne       short M01_L32
       vpackuswb xmm1,xmm0,xmm0
       vmovd     dword ptr [r8],xmm1
       add       rcx,8
       add       r8,4
       add       r9d,0FFFFFFFC
       vpextrq   r10,xmm0,1
M01_L32:
       mov       ebx,r10d
       test      ebx,0FF80FF80
       jne       short M01_L33
       mov       esi,ebx
       shr       esi,8
       or        esi,ebx
       mov       [r8],si
       add       rcx,4
       add       r8,2
       add       r9d,0FFFFFFFE
       shr       r10,20
       mov       ebx,r10d
M01_L33:
       test      ebx,0FF80
       jne       short M01_L34
       test      r9d,r9d
       je        near ptr M01_L69
       jmp       short M01_L35
M01_L34:
       test      ebx,0F800
       jne       near ptr M01_L51
       jmp       near ptr M01_L39
M01_L35:
       mov       [r8],bl
       add       rcx,2
       inc       r8
       dec       r9d
       cmp       rcx,rax
       ja        near ptr M01_L60
       mov       ebx,[rcx]
       jmp       short M01_L34
M01_L36:
       cmp       r9d,2
       jl        near ptr M01_L69
       jmp       short M01_L40
M01_L37:
       cmp       r9d,4
       jl        short M01_L38
       mov       r10d,ebx
       shr       r10d,6
       and       r10d,1F001F
       shl       ebx,8
       and       ebx,3F003F00
       add       r10d,ebx
       add       r10d,80C080C0
       mov       [r8],r10d
       add       rcx,4
       add       r8,4
       add       r9d,0FFFFFFFC
       cmp       rcx,rax
       ja        near ptr M01_L60
       mov       ebx,[rcx]
       lea       r10d,[rbx-80]
       movzx     r10d,r10w
       cmp       r10d,780
       jl        short M01_L39
       mov       r10d,ebx
       jmp       near ptr M01_L50
M01_L38:
       mov       r10d,ebx
       jmp       near ptr M01_L62
M01_L39:
       lea       r10d,[rbx-800000]
       cmp       r10d,77FFFFF
       jbe       short M01_L37
       jmp       short M01_L36
M01_L40:
       lea       r10d,[rbx*4]
       and       r10d,1F00
       mov       esi,ebx
       and       esi,3F
       lea       r10d,[r10+rsi+0C080]
       movbe     [r8],r10w
       cmp       ebx,800000
       jb        short M01_L41
       add       rcx,2
       add       r8,2
       add       r9d,0FFFFFFFE
       cmp       rcx,rax
       ja        near ptr M01_L60
       jmp       short M01_L42
M01_L41:
       cmp       r9d,3
       jl        near ptr M01_L59
       jmp       near ptr M01_L54
M01_L42:
       mov       ebx,[rcx]
       jmp       near ptr M01_L51
M01_L43:
       test      ebx,0F8000000
       jne       short M01_L45
       jmp       short M01_L46
M01_L44:
       lea       r10d,[rbx+23FF2800]
       test      r10d,0FC00FC00
       je        near ptr M01_L56
       jmp       near ptr M01_L55
M01_L45:
       lea       r10d,[rbx+28000000]
       cmp       r10d,8000000
       jb        short M01_L46
       cmp       r9d,6
       jge       short M01_L47
M01_L46:
       cmp       r9d,3
       jl        near ptr M01_L69
       jmp       near ptr M01_L48
M01_L47:
       lea       r10d,[rbx*4]
       and       r10d,3F00
       mov       esi,ebx
       and       esi,3F
       shl       esi,10
       or        r10d,esi
       mov       esi,ebx
       shr       esi,4
       and       esi,0F000000
       mov       edi,ebx
       shr       edi,0C
       and       edi,0F
       or        esi,edi
       add       r10d,esi
       add       r10d,0E08080E0
       mov       [r8],r10d
       mov       r10d,ebx
       shr       r10d,16
       and       r10d,3F
       shr       ebx,8
       and       ebx,3F00
       add       r10d,ebx
       add       r10d,8080
       mov       [r8+4],r10w
       add       rcx,4
       add       r8,6
       add       r9d,0FFFFFFFA
       cmp       rcx,rax
       ja        near ptr M01_L60
       mov       ebx,[rcx]
       test      ebx,0F800
       jne       near ptr M01_L51
       mov       r10d,ebx
       jmp       near ptr M01_L50
M01_L48:
       lea       r10d,[rbx*4]
       and       r10d,3F00
       movzx     esi,bx
       shr       esi,0C
       add       r10d,esi
       add       r10d,80E0
       mov       [r8],r10w
       mov       r10d,ebx
       and       r10d,3F
       or        r10d,0FFFFFF80
       mov       [r8+2],r10b
       add       rcx,2
       add       r8,3
       add       r9d,0FFFFFFFD
       cmp       ebx,800000
       jb        short M01_L49
       cmp       rcx,rax
       ja        near ptr M01_L60
       jmp       short M01_L52
M01_L49:
       test      r9d,r9d
       je        near ptr M01_L69
       mov       r10d,ebx
       shr       r10d,10
       mov       [r8],r10b
       add       rcx,2
       inc       r8
       dec       r9d
       cmp       rcx,rax
       ja        near ptr M01_L60
       mov       ebx,[rcx]
       test      ebx,0F800
       jne       short M01_L51
       mov       r10d,ebx
M01_L50:
       test      r10d,0FF80FF80
       je        near ptr M01_L26
       mov       ebx,r10d
       jmp       near ptr M01_L33
M01_L51:
       lea       r10d,[rbx-0D800]
       test      r10d,0F800
       je        near ptr M01_L44
       jmp       near ptr M01_L43
M01_L52:
       mov       ebx,[rcx]
       jmp       near ptr M01_L33
M01_L53:
       vmovq     xmm0,r10
       vpackuswb xmm0,xmm0,xmm0
       vmovd     dword ptr [r8],xmm0
       add       rcx,8
       jmp       short M01_L57
M01_L54:
       mov       r10d,ebx
       shr       r10d,10
       mov       [r8+2],r10b
       add       rcx,4
       add       r8,3
       add       r9d,0FFFFFFFD
       jmp       short M01_L58
M01_L55:
       mov       eax,3
       jmp       near ptr M01_L70
M01_L56:
       cmp       r9d,4
       jl        near ptr M01_L69
       lea       r10d,[rbx+40]
       mov       ebx,r10d
       and       ebx,3
       shl       ebx,14
       or        ebx,808080F0
       mov       esi,r10d
       and       esi,3F0700
       bswap     esi
       rol       esi,10
       or        ebx,esi
       mov       esi,r10d
       shr       esi,6
       and       esi,0F0000
       or        ebx,esi
       and       r10d,0FC
       shl       r10d,6
       or        r10d,ebx
       mov       [r8],r10d
       add       rcx,4
M01_L57:
       add       r8,4
       add       r9d,0FFFFFFFC
M01_L58:
       cmp       rcx,rax
       jbe       near ptr M01_L25
       jmp       short M01_L60
M01_L59:
       add       rcx,2
       add       r8,2
       jmp       near ptr M01_L69
M01_L60:
       sub       rax,rcx
       mov       rdx,rax
       shr       rdx,3F
       add       rdx,rax
       sar       rdx,1
       add       edx,2
M01_L61:
       test      edx,edx
       je        near ptr M01_L68
       movzx     r10d,word ptr [rcx]
       jmp       short M01_L63
M01_L62:
       movzx     r10d,r10w
M01_L63:
       cmp       r10d,7F
       ja        short M01_L64
       test      r9d,r9d
       je        near ptr M01_L69
       mov       [r8],r10b
       add       rcx,2
       inc       r8
       jmp       near ptr M01_L67
M01_L64:
       cmp       r10d,800
       jae       short M01_L65
       cmp       r9d,2
       jl        near ptr M01_L69
       mov       r9d,r10d
       and       r9d,3F
       or        r9d,0FFFFFF80
       mov       [r8+1],r9b
       shr       r10d,6
       or        r10d,0FFFFFFC0
       mov       [r8],r10b
       add       rcx,2
       add       r8,2
       jmp       short M01_L67
M01_L65:
       lea       eax,[r10-0D800]
       cmp       eax,7FF
       jbe       short M01_L66
       cmp       r9d,3
       jl        short M01_L69
       mov       eax,r10d
       and       eax,3F
       or        eax,0FFFFFF80
       mov       [r8+2],al
       mov       eax,r10d
       shr       eax,6
       and       eax,3F
       or        eax,0FFFFFF80
       mov       [r8+1],al
       shr       r10d,0C
       or        r10d,0FFFFFFE0
       mov       [r8],r10b
       add       rcx,2
       add       r8,3
       jmp       short M01_L67
M01_L66:
       cmp       r10d,0DBFF
       ja        near ptr M01_L55
       mov       eax,2
       jmp       short M01_L70
M01_L67:
       cmp       edx,1
       jg        short M01_L69
M01_L68:
       xor       eax,eax
       jmp       short M01_L70
M01_L69:
       mov       eax,1
M01_L70:
       mov       r11,[rsp+40]
       mov       [r11],rcx
       mov       rcx,[rsp+48]
       mov       [rcx],r8
       vzeroupper
       pop       rbx
       pop       rsi
       pop       rdi
       ret
; Total bytes of code 2081
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
       call      qword ptr [7FFC546BFF00]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 48
```
```asm
; Bshox.Internals.EncodingHelper.Utf8Encode(System.ReadOnlySpan`1<Char>, Byte ByRef, Int32)
;     {
;     ^
;         fixed (char* charsPtr = &MemoryMarshal.GetReference(chars))
;                ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         fixed (byte* bytesPtr = &bytes)
;                ^^^^^^^^^^^^^^^^^^^^^^^
;             return Utf8NoBom.GetBytes(charsPtr, chars.Length, bytesPtr, byteCount);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       sub       rsp,38
       mov       r9,[rcx]
       mov       [rsp+30],r9
       mov       rax,r9
       mov       [rsp+28],rdx
       mov       r9d,[rcx+8]
       mov       [rsp+20],r8d
       mov       r8d,r9d
       mov       r9,rdx
       mov       rcx,13177C012F8
       mov       rcx,[rcx]
       mov       rdx,rax
       call      qword ptr [7FFC5445CCA8]; System.Text.UTF8Encoding.GetBytes(Char*, Int32, Byte*, Int32)
       nop
       add       rsp,38
       ret
; Total bytes of code 63
```

## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: DefaultJob)

```asm
; Benchmark.WriteString.Unicode()
;         var writer = new BshoxWriter(fixedBufferWriter);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         for (int i = 0; i < Count; i++)
;              ^^^^^^^^^
;             writer.WriteString(_stringsUnicode[i]);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         fixedBufferWriter.Reset();
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^
;         return writer.UnflushedBytes;
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       push      rbp
       push      r15
       push      r14
       push      r13
       push      r12
       push      rdi
       push      rsi
       push      rbx
       sub       rsp,178
       lea       rbp,[rsp+1B0]
       xor       eax,eax
       mov       [rbp-148],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rbp-140],xmm4
       mov       rax,0FFFFFFFFFFFFFF10
M00_L00:
       vmovdqa   xmmword ptr [rbp+rax-40],xmm4
       vmovdqa   xmmword ptr [rbp+rax-30],xmm4
       vmovdqa   xmmword ptr [rbp+rax-20],xmm4
       add       rax,30
       jne       short M00_L00
       mov       rsi,rcx
       mov       rdi,[rsi+18]
       xor       ebx,ebx
       xor       r14d,r14d
       xor       r15d,r15d
       mov       r8,2648E0012F0
       mov       r13,[r8]
       xor       r12d,r12d
       jmp       near ptr M00_L06
M00_L01:
       lea       edx,[rcx+1]
       mov       r8d,[rbp-8C]
       mov       r11d,r8d
       or        r11d,0FFFFFF80
       movsxd    rcx,ecx
       mov       [rbx+rcx],r11b
       shr       r8d,7
       cmp       r8d,7F
       mov       [rbp-8C],r8d
       mov       ecx,edx
       ja        short M00_L01
M00_L02:
       movsxd    rdx,ecx
       mov       r8d,[rbp-8C]
       mov       [rbx+rdx],r8b
       inc       ecx
       mov       [rbp-0C4],ecx
       test      ecx,ecx
       jl        near ptr M00_L50
       mov       edx,ecx
       add       rbx,rdx
       sub       r14d,ecx
       add       r15d,ecx
       cmp       r14d,eax
       jl        near ptr M00_L15
M00_L03:
       mov       r10,[rbp-158]
       add       r10,0C
       mov       r11d,[rbp-14C]
       mov       [rbp-11C],r11d
       xor       ecx,ecx
       mov       [rbp-100],rcx
       mov       [rbp-108],rcx
       mov       [rbp-100],r10
       mov       [rbp-130],r10
       mov       [rbp-108],rbx
       mov       r9,rbx
       mov       [rbp-138],r9
       test      r10,r10
       je        near ptr M00_L20
       test      r9,r9
       je        near ptr M00_L52
       mov       eax,[rbp-3C]
       mov       ecx,r11d
       or        ecx,eax
       jl        near ptr M00_L53
       mov       r8,[rbp-170]
       mov       [rbp-168],r8
       lea       rcx,[rbp-110]
       mov       [rsp+20],rcx
       lea       rcx,[rbp-118]
       mov       [rsp+28],rcx
       mov       rcx,r10
       mov       edx,r11d
       mov       r8,r9
       mov       r9d,eax
       call      qword ptr [7FFC54327C00]; System.Text.Unicode.Utf8Utility.TranscodeToUtf8(Char*, Int32, Byte*, Int32, Char* ByRef, Byte* ByRef)
       mov       rcx,[rbp-110]
       mov       rdx,[rbp-130]
       sub       rcx,rdx
       mov       r8,rcx
       shr       r8,3F
       add       rcx,r8
       sar       rcx,1
       mov       r8d,[rbp-118]
       mov       r9,[rbp-138]
       sub       r8d,r9d
       mov       r11d,[rbp-11C]
       cmp       ecx,r11d
       jne       near ptr M00_L47
M00_L04:
       xor       ecx,ecx
       mov       [rbp-100],rcx
       mov       [rbp-108],rcx
       mov       ecx,[rbp-3C]
       test      ecx,ecx
       jl        near ptr M00_L54
       mov       edx,ecx
       add       rbx,rdx
       sub       r14d,ecx
       add       r15d,ecx
M00_L05:
       inc       r12d
       cmp       r12d,3E8
       jge       near ptr M00_L09
M00_L06:
       mov       r8,[rsi+8]
       cmp       r12d,[r8+8]
       jae       near ptr M00_L55
       mov       rax,[r8+r12*8+10]
       mov       [rbp-158],rax
       mov       r10d,[rax+8]
       mov       [rbp-14C],r10d
       test      r10d,r10d
       je        near ptr M00_L22
       cmp       r10d,2A
       jle       near ptr M00_L27
       lea       r8,[rax+0C]
       mov       [rbp-70],r8
       mov       r11,[rbp-70]
       mov       [rbp-128],r11
       mov       r8,2648E0012F8
       mov       rdx,[r8]
       mov       [rbp-170],rdx
       mov       [rbp-160],rdx
       mov       [rbp-74],r10d
       lea       r8,[rbp-80]
       lea       r9,[rbp-88]
       mov       rcx,r11
       mov       edx,[rbp-74]
       call      qword ptr [7FFC54327AE0]; System.Text.Unicode.Utf16Utility.GetPointerToFirstInvalidChar(Char*, Int32, Int64 ByRef, Int32 ByRef)
       mov       rdx,[rbp-128]
       sub       rax,rdx
       mov       r9,rax
       shr       r9,3F
       add       r9,rax
       sar       r9,1
       movsxd    rax,r9d
       add       rax,[rbp-80]
       cmp       rax,7FFFFFFF
       ja        near ptr M00_L49
       mov       [rbp-78],eax
       mov       r8d,[rbp-74]
       cmp       r9d,r8d
       jne       near ptr M00_L32
M00_L07:
       xor       ecx,ecx
       mov       [rbp-70],rcx
       mov       eax,[rbp-78]
       mov       [rbp-3C],eax
       mov       [rbp-8C],eax
       cmp       r14d,5
       jl        short M00_L10
M00_L08:
       xor       ecx,ecx
       mov       eax,[rbp-3C]
       cmp       eax,7F
       jbe       near ptr M00_L02
       jmp       near ptr M00_L01
M00_L09:
       mov       rdi,[rsi+18]
       lea       rsi,[rdi+18]
       add       rdi,8
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,r15d
       add       rsp,178
       pop       rbx
       pop       rsi
       pop       rdi
       pop       r12
       pop       r13
       pop       r14
       pop       r15
       pop       rbp
       ret
M00_L10:
       test      r15d,r15d
       jg        near ptr M00_L33
M00_L11:
       mov       edx,[r13+0C]
       cmp       edx,5
       jle       near ptr M00_L35
       jmp       near ptr M00_L34
M00_L12:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rdi],rdx
       jne       near ptr M00_L39
       lea       rdx,[rbp-0B0]
       mov       rcx,rdi
       mov       r8d,r10d
       call      qword ptr [7FFC54586160]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       ebx,ebx
       xor       r14d,r14d
       mov       rcx,[rbp-0B0]
       test      rcx,rcx
       jne       near ptr M00_L36
M00_L13:
       mov       [rbp-0A0],rbx
       mov       [rbp-98],r14d
M00_L14:
       cmp       dword ptr [rbp-98],0
       jbe       near ptr M00_L55
       mov       rbx,[rbp-0A0]
       mov       r14d,[rbp-98]
       jmp       near ptr M00_L08
M00_L15:
       test      r15d,r15d
       jg        near ptr M00_L40
M00_L16:
       mov       r8d,[r13+0C]
       mov       eax,[rbp-3C]
       cmp       eax,r8d
       jge       near ptr M00_L42
       jmp       near ptr M00_L41
M00_L17:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rdi],rdx
       jne       near ptr M00_L46
       lea       rdx,[rbp-0E8]
       mov       rcx,rdi
       call      qword ptr [7FFC54586160]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       ebx,ebx
       xor       r14d,r14d
       mov       rcx,[rbp-0E8]
       test      rcx,rcx
       jne       near ptr M00_L43
M00_L18:
       mov       rdx,rbx
       mov       [rbp-0D8],rdx
       mov       [rbp-0D0],r14d
M00_L19:
       cmp       dword ptr [rbp-0D0],0
       jbe       near ptr M00_L55
       mov       rbx,[rbp-0D8]
       mov       r14d,[rbp-0D0]
       jmp       near ptr M00_L03
M00_L20:
       mov       ecx,0E
M00_L21:
       mov       edx,31
       call      qword ptr [7FFC54704168]
       int       3
M00_L22:
       test      r14d,r14d
       jg        short M00_L26
       test      r15d,r15d
       jle       short M00_L23
       mov       rcx,rdi
       mov       edx,r15d
       mov       r11,7FFC54270588
       call      qword ptr [r11]
       xor       r15d,r15d
M00_L23:
       mov       r8d,[r13+0C]
       cmp       r8d,1
       jle       short M00_L24
       jmp       short M00_L25
M00_L24:
       mov       r8d,1
M00_L25:
       lea       rdx,[rbp-50]
       mov       rcx,rdi
       mov       r11,7FFC54270580
       call      qword ptr [r11]
       cmp       dword ptr [rbp-48],0
       jbe       near ptr M00_L55
       mov       rbx,[rbp-50]
       mov       r14d,[rbp-48]
M00_L26:
       mov       byte ptr [rbx],0
       inc       rbx
       dec       r14d
       inc       r15d
       jmp       near ptr M00_L05
M00_L27:
       cmp       r14d,80
       jge       short M00_L31
       test      r15d,r15d
       jle       short M00_L28
       mov       rcx,rdi
       mov       edx,r15d
       mov       r11,7FFC54270598
       call      qword ptr [r11]
       xor       r15d,r15d
       mov       rax,[rbp-158]
       mov       r10d,[rbp-14C]
M00_L28:
       mov       r8d,[r13+0C]
       cmp       r8d,80
       jle       short M00_L29
       jmp       short M00_L30
M00_L29:
       mov       r8d,80
M00_L30:
       lea       rdx,[rbp-60]
       mov       rcx,rdi
       mov       r11,7FFC54270590
       call      qword ptr [r11]
       cmp       dword ptr [rbp-58],0
       jbe       near ptr M00_L55
       mov       rbx,[rbp-60]
       mov       r14d,[rbp-58]
       mov       rax,[rbp-158]
       mov       r10d,[rbp-14C]
M00_L31:
       lea       rcx,[rax+0C]
       mov       edx,r10d
       mov       [rbp-148],rcx
       mov       [rbp-140],edx
       lea       rcx,[rbp-148]
       lea       rdx,[rbx+1]
       mov       r8d,7F
       call      qword ptr [7FFC5468FDF8]; Bshox.Internals.EncodingHelper.Utf8Encode(System.ReadOnlySpan`1<Char>, Byte ByRef, Int32)
       mov       [rbx],al
       lea       ecx,[rax+1]
       mov       [rbp-64],ecx
       test      ecx,ecx
       jl        near ptr M00_L48
       mov       edx,ecx
       add       rbx,rdx
       sub       r14d,ecx
       add       r15d,ecx
       jmp       near ptr M00_L05
M00_L32:
       mov       rcx,[rbp-160]
       call      qword ptr [7FFC54704150]
       add       eax,[rbp-78]
       mov       ecx,eax
       test      ecx,ecx
       mov       [rbp-78],ecx
       jge       near ptr M00_L07
       jmp       near ptr M00_L49
M00_L33:
       mov       rcx,rdi
       mov       edx,r15d
       mov       r11,7FFC542705A8
       call      qword ptr [r11]
       xor       r15d,r15d
       jmp       near ptr M00_L11
M00_L34:
       mov       r10d,edx
       jmp       near ptr M00_L12
M00_L35:
       mov       r10d,5
       jmp       near ptr M00_L12
M00_L36:
       mov       rax,[rcx]
       mov       rdx,rax
       test      dword ptr [rdx],80000000
       je        short M00_L37
       lea       rbx,[rcx+10]
       mov       r14d,[rcx+8]
       jmp       short M00_L38
M00_L37:
       lea       rdx,[rbp-0C0]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       r14d,[rbp-0B8]
       mov       rbx,[rbp-0C0]
M00_L38:
       mov       edx,[rbp-0A8]
       and       edx,7FFFFFFF
       mov       ecx,[rbp-0A4]
       mov       r8d,ecx
       add       r8,rdx
       mov       r11d,r14d
       cmp       r8,r11
       ja        near ptr M00_L51
       add       rbx,rdx
       mov       r14d,ecx
       jmp       near ptr M00_L13
M00_L39:
       lea       rdx,[rbp-0A0]
       mov       rcx,rdi
       mov       r8d,r10d
       mov       r11,7FFC542705A0
       call      qword ptr [r11]
       jmp       near ptr M00_L14
M00_L40:
       mov       rcx,rdi
       mov       edx,r15d
       mov       r11,7FFC542705B8
       call      qword ptr [r11]
       xor       r15d,r15d
       jmp       near ptr M00_L16
M00_L41:
       jmp       near ptr M00_L17
M00_L42:
       mov       r8d,eax
       mov       eax,[rbp-3C]
       jmp       near ptr M00_L17
M00_L43:
       mov       rax,[rcx]
       mov       rdx,rax
       test      dword ptr [rdx],80000000
       je        short M00_L44
       lea       rbx,[rcx+10]
       mov       r14d,[rcx+8]
       jmp       short M00_L45
M00_L44:
       lea       rdx,[rbp-0F8]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       r14d,[rbp-0F0]
       mov       rbx,[rbp-0F8]
M00_L45:
       mov       edx,[rbp-0E0]
       and       edx,7FFFFFFF
       mov       ecx,[rbp-0DC]
       mov       r8d,ecx
       add       r8,rdx
       mov       r11d,r14d
       cmp       r8,r11
       ja        near ptr M00_L51
       add       rbx,rdx
       mov       r14d,ecx
       jmp       near ptr M00_L18
M00_L46:
       lea       rdx,[rbp-0D8]
       mov       rcx,rdi
       mov       r11,7FFC542705B0
       call      qword ptr [r11]
       jmp       near ptr M00_L19
M00_L47:
       mov       eax,[rbp-3C]
       mov       [rsp+20],eax
       mov       [rsp+28],ecx
       mov       [rsp+30],r8d
       mov       dword ptr [rsp+38],1
       mov       rcx,[rbp-168]
       mov       r8d,r11d
       call      qword ptr [7FFC54704138]
       jmp       near ptr M00_L04
M00_L48:
       mov       ecx,225
       mov       rdx,7FFC546E42F8
       call      qword ptr [7FFC5432EE38]
       mov       rdx,rax
       mov       ecx,[rbp-64]
       call      qword ptr [7FFC54704108]
       int       3
M00_L49:
       call      qword ptr [7FFC54704180]
       int       3
M00_L50:
       mov       ecx,225
       mov       rdx,7FFC546E42F8
       call      qword ptr [7FFC5432EE38]
       mov       rdx,rax
       mov       ecx,[rbp-0C4]
       call      qword ptr [7FFC54704108]
       int       3
M00_L51:
       call      qword ptr [7FFC544F7990]
       int       3
M00_L52:
       mov       ecx,0A
       jmp       near ptr M00_L21
M00_L53:
       mov       ecx,0C
       mov       edx,0D
       call      qword ptr [7FFC54685770]
       int       3
M00_L54:
       mov       ecx,225
       mov       rdx,7FFC546E42F8
       call      qword ptr [7FFC5432EE38]
       mov       rdx,rax
       mov       ecx,[rbp-3C]
       call      qword ptr [7FFC54704108]
       int       3
M00_L55:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 1924
```
```asm
; System.Text.Unicode.Utf8Utility.TranscodeToUtf8(Char*, Int32, Byte*, Int32, Char* ByRef, Byte* ByRef)
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       mov       r10,[rsp+48]
       mov       r11,[rsp+50]
       cmp       edx,r9d
       mov       eax,r9d
       cmovle    eax,edx
       xor       ebx,ebx
       cmp       rax,20
       jb        near ptr M01_L32
       mov       rsi,[rcx]
       mov       rdi,0FF80FF80FF80FF80
       test      rdi,rsi
       je        near ptr M01_L19
M01_L00:
       mov       eax,esi
       test      eax,0FF80FF80
       je        near ptr M01_L39
M01_L01:
       test      eax,0FF80
       je        near ptr M01_L40
M01_L02:
       lea       rcx,[rcx+rbx*2]
       add       r8,rbx
       cmp       ebx,edx
       je        near ptr M01_L41
       sub       edx,ebx
       sub       r9d,ebx
       cmp       edx,2
       jl        short M01_L04
       mov       eax,edx
       lea       rax,[rcx+rax*2-4]
       jmp       near ptr M01_L17
M01_L03:
       sub       rax,rcx
       mov       rdx,rax
       shr       rdx,3F
       add       rdx,rax
       sar       rdx,1
       add       edx,2
M01_L04:
       test      edx,edx
       je        short M01_L07
       movzx     eax,word ptr [rcx]
M01_L05:
       cmp       eax,7F
       jbe       near ptr M01_L59
       cmp       eax,800
       jb        near ptr M01_L18
       lea       ebx,[rax-0D800]
       cmp       ebx,7FF
       jbe       near ptr M01_L60
       cmp       r9d,3
       jl        near ptr M01_L61
       mov       r9d,eax
       and       r9d,3F
       or        r9d,0FFFFFF80
       mov       [r8+2],r9b
       mov       r9d,eax
       shr       r9d,6
       and       r9d,3F
       or        r9d,0FFFFFF80
       mov       [r8+1],r9b
       shr       eax,0C
       or        eax,0FFFFFFE0
       mov       [r8],al
       add       rcx,2
       add       r8,3
M01_L06:
       cmp       edx,1
       jg        near ptr M01_L61
M01_L07:
       xor       eax,eax
M01_L08:
       mov       [r10],rcx
       mov       [r11],r8
       vzeroupper
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M01_L09:
       cmp       r9d,3
       jl        near ptr M01_L61
       lea       esi,[rbx*4]
       and       esi,3F00
       movzx     edi,bx
       shr       edi,0C
       add       esi,edi
       add       esi,80E0
       mov       [r8],si
       mov       esi,ebx
       and       esi,3F
       or        esi,0FFFFFF80
       mov       [r8+2],sil
       add       rcx,2
       add       r8,3
       add       r9d,0FFFFFFFD
       cmp       ebx,800000
       jae       near ptr M01_L51
       test      r9d,r9d
       je        near ptr M01_L61
       shr       ebx,10
       mov       [r8],bl
       add       rcx,2
       inc       r8
       dec       r9d
       cmp       rcx,rax
       ja        near ptr M01_L03
       mov       ebx,[rcx]
       test      ebx,0F800
       je        near ptr M01_L11
M01_L10:
       lea       esi,[rbx-0D800]
       test      esi,0F800
       je        near ptr M01_L53
       test      ebx,0F8000000
       je        near ptr M01_L09
       lea       esi,[rbx+28000000]
       cmp       esi,8000000
       jb        near ptr M01_L09
       cmp       r9d,6
       jl        near ptr M01_L09
       lea       esi,[rbx*4]
       and       esi,3F00
       mov       edi,ebx
       and       edi,3F
       shl       edi,10
       or        esi,edi
       mov       edi,ebx
       shr       edi,4
       and       edi,0F000000
       mov       ebp,ebx
       shr       ebp,0C
       and       ebp,0F
       or        edi,ebp
       add       esi,edi
       add       esi,0E08080E0
       mov       [r8],esi
       mov       esi,ebx
       shr       esi,16
       and       esi,3F
       shr       ebx,8
       and       ebx,3F00
       add       ebx,esi
       add       ebx,8080
       mov       [r8+4],bx
       add       rcx,4
       add       r8,6
       add       r9d,0FFFFFFFA
       cmp       rcx,rax
       ja        near ptr M01_L03
       mov       ebx,[rcx]
       test      ebx,0F800
       jne       near ptr M01_L10
M01_L11:
       test      ebx,0FF80FF80
       je        near ptr M01_L42
M01_L12:
       test      ebx,0FF80
       jne       short M01_L13
       test      r9d,r9d
       je        near ptr M01_L61
       mov       [r8],bl
       add       rcx,2
       inc       r8
       dec       r9d
       cmp       rcx,rax
       ja        near ptr M01_L03
       mov       ebx,[rcx]
M01_L13:
       test      ebx,0F800
       jne       near ptr M01_L10
       lea       esi,[rbx-800000]
       cmp       esi,77FFFFF
       ja        short M01_L15
M01_L14:
       cmp       r9d,4
       jl        near ptr M01_L58
       mov       esi,ebx
       shr       esi,6
       and       esi,1F001F
       shl       ebx,8
       and       ebx,3F003F00
       add       ebx,esi
       add       ebx,80C080C0
       mov       [r8],ebx
       add       rcx,4
       add       r8,4
       add       r9d,0FFFFFFFC
       cmp       rcx,rax
       ja        near ptr M01_L03
       mov       ebx,[rcx]
       lea       esi,[rbx-80]
       movzx     esi,si
       cmp       esi,780
       jge       near ptr M01_L11
       lea       esi,[rbx-800000]
       cmp       esi,77FFFFF
       jbe       short M01_L14
M01_L15:
       cmp       r9d,2
       jl        near ptr M01_L61
       lea       esi,[rbx*4]
       and       esi,1F00
       mov       edi,ebx
       and       edi,3F
       lea       esi,[rsi+rdi+0C080]
       movbe     [r8],si
       cmp       ebx,800000
       jae       near ptr M01_L49
       cmp       r9d,3
       jl        near ptr M01_L57
       shr       ebx,10
       mov       [r8+2],bl
       add       rcx,4
       add       r8,3
       add       r9d,0FFFFFFFD
M01_L16:
       cmp       rcx,rax
       ja        near ptr M01_L03
M01_L17:
       mov       ebx,[rcx]
       jmp       near ptr M01_L11
M01_L18:
       cmp       r9d,2
       jl        near ptr M01_L61
       mov       r9d,eax
       and       r9d,3F
       or        r9d,0FFFFFF80
       mov       [r8+1],r9b
       shr       eax,6
       or        eax,0FFFFFFC0
       mov       [r8],al
       add       rcx,2
       add       r8,2
       jmp       near ptr M01_L06
M01_L19:
       cmp       rax,40
       jb        near ptr M01_L26
       mov       rbx,rcx
       vmovups   ymm0,[rbx]
       vptest    ymm0,ymmword ptr [7FFC5439E500]
       je        short M01_L20
       xor       ebx,ebx
       jmp       near ptr M01_L25
M01_L20:
       mov       rsi,r8
       vpackuswb ymm0,ymm0,ymm0
       vpermq    ymm0,ymm0,0D8
       vmovups   [rsi],xmm0
       mov       edi,10
       test      r8b,10
       jne       short M01_L21
       vmovups   ymm0,[rbx+20]
       vptest    ymm0,ymmword ptr [7FFC5439E500]
       jne       short M01_L23
       vpackuswb ymm0,ymm0,ymm0
       vpermq    ymm0,ymm0,0D8
       vmovups   [rsi+10],xmm0
M01_L21:
       mov       rdi,r8
       and       rdi,1F
       neg       rdi
       add       rdi,20
       lea       rbp,[rax-20]
M01_L22:
       vmovups   ymm0,[rbx+rdi*2]
       vmovups   ymm1,[rbx+rdi*2+20]
       vpor      ymm2,ymm0,ymm1
       vptest    ymm2,ymmword ptr [7FFC5439E500]
       jne       short M01_L24
       vpackuswb ymm0,ymm0,ymm1
       vpermq    ymm0,ymm0,0D8
       vmovups   [rsi+rdi],ymm0
       add       rdi,20
       cmp       rdi,rbp
       jbe       short M01_L22
M01_L23:
       mov       rbx,rdi
       jmp       short M01_L25
M01_L24:
       vptest    ymm0,ymmword ptr [7FFC5439E500]
       jne       short M01_L23
       vpackuswb ymm0,ymm0,ymm0
       vpermq    ymm0,ymm0,0D8
       vmovups   [rsi+rdi],xmm0
       add       rdi,10
       jmp       short M01_L23
M01_L25:
       jmp       near ptr M01_L32
M01_L26:
       mov       rbx,rcx
       vmovups   xmm0,[rbx]
       vptest    xmm0,xmmword ptr [7FFC5439E500]
       je        short M01_L27
       xor       ebx,ebx
       jmp       near ptr M01_L32
M01_L27:
       mov       rsi,r8
       vpackuswb xmm0,xmm0,xmm0
       vmovsd    qword ptr [rsi],xmm0
       mov       edi,8
       test      r8b,8
       jne       short M01_L28
       vmovups   xmm0,[rbx+10]
       vptest    xmm0,xmmword ptr [7FFC5439E500]
       jne       short M01_L30
       vpackuswb xmm0,xmm0,xmm0
       vmovsd    qword ptr [rsi+8],xmm0
M01_L28:
       mov       rdi,r8
       and       rdi,0F
       neg       rdi
       add       rdi,10
       lea       rbp,[rax-10]
M01_L29:
       vmovups   xmm0,[rbx+rdi*2]
       vmovups   xmm1,[rbx+rdi*2+10]
       vpor      xmm2,xmm0,xmm1
       vptest    xmm2,xmmword ptr [7FFC5439E500]
       jne       short M01_L31
       vpackuswb xmm0,xmm0,xmm1
       vmovups   [rsi+rdi],xmm0
       add       rdi,10
       cmp       rdi,rbp
       jbe       short M01_L29
M01_L30:
       mov       rbx,rdi
       jmp       short M01_L32
M01_L31:
       vptest    xmm0,xmmword ptr [7FFC5439E500]
       jne       short M01_L30
       vpackuswb xmm0,xmm0,xmm0
       vmovsd    qword ptr [rsi+rdi],xmm0
       add       rdi,8
       jmp       short M01_L30
M01_L32:
       sub       rax,rbx
       cmp       rax,4
       jb        short M01_L34
       lea       rsi,[rbx+rax-4]
M01_L33:
       mov       rdi,[rcx+rbx*2]
       mov       rbp,0FF80FF80FF80FF80
       test      rbp,rdi
       jne       short M01_L38
       vmovq     xmm0,rdi
       vpackuswb xmm0,xmm0,xmm0
       vmovd     dword ptr [r8+rbx],xmm0
       add       rbx,4
       cmp       rbx,rsi
       jbe       short M01_L33
M01_L34:
       test      al,2
       je        short M01_L37
       mov       esi,[rcx+rbx*2]
       test      esi,0FF80FF80
       jne       short M01_L36
       mov       rbp,rax
       mov       eax,esi
       mov       rsi,rbp
       lea       rdi,[r8+rbx]
       mov       [rdi],al
       shr       eax,10
       mov       [rdi+1],al
       add       rbx,2
M01_L35:
       test      sil,1
       je        near ptr M01_L02
       movzx     eax,word ptr [rcx+rbx*2]
       cmp       eax,7F
       ja        near ptr M01_L02
       jmp       short M01_L40
M01_L36:
       mov       eax,esi
       jmp       near ptr M01_L01
M01_L37:
       mov       rsi,rax
       jmp       short M01_L35
M01_L38:
       mov       rsi,rdi
       jmp       near ptr M01_L00
M01_L39:
       lea       rdi,[r8+rbx]
       mov       [rdi],al
       shr       eax,10
       mov       [rdi+1],al
       shr       rsi,20
       mov       eax,esi
       add       rbx,2
       jmp       near ptr M01_L01
M01_L40:
       mov       [r8+rbx],al
       inc       rbx
       jmp       near ptr M01_L02
M01_L41:
       mov       [r10],rcx
       mov       [r11],r8
       xor       eax,eax
       vzeroupper
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M01_L42:
       cmp       r9d,2
       jl        near ptr M01_L58
       mov       esi,ebx
       shr       esi,8
       or        esi,ebx
       mov       [r8],si
       add       rcx,4
       add       r8,2
       add       r9d,0FFFFFFFE
       mov       rbx,rax
       sub       rbx,rcx
       mov       rsi,rbx
       shr       rsi,3F
       add       rbx,rsi
       sar       rbx,1
       add       ebx,2
       movsxd    rsi,r9d
       cmp       rbx,rsi
       jle       short M01_L43
       jmp       short M01_L44
M01_L43:
       mov       rsi,rbx
M01_L44:
       mov       ebx,esi
       shr       ebx,3
       xor       edi,edi
       jmp       short M01_L46
M01_L45:
       vmovups   xmm0,[rcx]
       vptest    xmm0,xmmword ptr [7FFC5439E500]
       jne       short M01_L47
       vpackuswb xmm0,xmm0,xmm0
       vmovq     qword ptr [r8],xmm0
       add       rcx,10
       add       r8,8
       inc       edi
M01_L46:
       cmp       edi,ebx
       jb        short M01_L45
       lea       ebx,[rdi*8]
       sub       r9d,ebx
       test      sil,4
       je        near ptr M01_L16
       mov       rbx,[rcx]
       mov       rsi,0FF80FF80FF80FF80
       test      rsi,rbx
       jne       short M01_L48
       jmp       near ptr M01_L52
M01_L47:
       shl       edi,3
       sub       r9d,edi
       vmovq     rbx,xmm0
       mov       rsi,0FF80FF80FF80FF80
       test      rsi,rbx
       jne       short M01_L48
       vpackuswb xmm1,xmm0,xmm0
       vmovd     dword ptr [r8],xmm1
       add       rcx,8
       add       r8,4
       add       r9d,0FFFFFFFC
       vpextrq   rbx,xmm0,1
M01_L48:
       mov       esi,ebx
       test      esi,0FF80FF80
       jne       short M01_L50
       mov       edi,esi
       shr       edi,8
       or        edi,esi
       mov       [r8],di
       add       rcx,4
       add       r8,2
       add       r9d,0FFFFFFFE
       shr       rbx,20
       mov       esi,ebx
       mov       ebx,esi
       jmp       near ptr M01_L12
M01_L49:
       add       rcx,2
       add       r8,2
       add       r9d,0FFFFFFFE
       cmp       rcx,rax
       ja        near ptr M01_L03
       mov       ebx,[rcx]
       jmp       near ptr M01_L10
M01_L50:
       mov       ebx,esi
       jmp       near ptr M01_L12
M01_L51:
       cmp       rcx,rax
       ja        near ptr M01_L03
       mov       ebx,[rcx]
       jmp       near ptr M01_L12
M01_L52:
       vmovq     xmm0,rbx
       vpackuswb xmm0,xmm0,xmm0
       vmovd     dword ptr [r8],xmm0
       add       rcx,8
       jmp       short M01_L56
M01_L53:
       lea       esi,[rbx+23FF2800]
       test      esi,0FC00FC00
       jne       short M01_L54
       cmp       r9d,4
       jl        near ptr M01_L61
       jmp       short M01_L55
M01_L54:
       mov       eax,3
       jmp       near ptr M01_L08
M01_L55:
       add       ebx,40
       mov       esi,ebx
       and       esi,3
       shl       esi,14
       or        esi,808080F0
       mov       edi,ebx
       and       edi,3F0700
       bswap     edi
       rol       edi,10
       or        esi,edi
       mov       edi,ebx
       shr       edi,6
       and       edi,0F0000
       or        esi,edi
       and       ebx,0FC
       shl       ebx,6
       or        ebx,esi
       mov       [r8],ebx
       add       rcx,4
M01_L56:
       add       r8,4
       add       r9d,0FFFFFFFC
       jmp       near ptr M01_L16
M01_L57:
       add       rcx,2
       add       r8,2
       jmp       short M01_L61
M01_L58:
       movzx     eax,bx
       jmp       near ptr M01_L05
M01_L59:
       test      r9d,r9d
       je        short M01_L61
       mov       [r8],al
       add       rcx,2
       inc       r8
       jmp       near ptr M01_L06
M01_L60:
       cmp       eax,0DBFF
       ja        near ptr M01_L54
       mov       eax,2
       jmp       near ptr M01_L08
M01_L61:
       mov       eax,1
       jmp       near ptr M01_L08
; Total bytes of code 1965
```
```asm
; System.Text.Unicode.Utf16Utility.GetPointerToFirstInvalidChar(Char*, Int32, Int64 ByRef, Int32 ByRef)
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       mov       rax,rcx
       mov       r10d,edx
       mov       r11,rax
       cmp       r10,20
       jae       near ptr M02_L15
       cmp       r10,10
       jae       near ptr M02_L13
M02_L00:
       cmp       r10,4
       jb        short M02_L02
M02_L01:
       mov       r11d,[rax]
       mov       ebx,[rax+4]
       mov       esi,r11d
       or        esi,ebx
       test      esi,0FF80FF80
       jne       near ptr M02_L18
       add       rax,8
       add       r10,0FFFFFFFFFFFFFFFC
       cmp       r10,4
       jae       short M02_L01
M02_L02:
       test      r10b,2
       je        short M02_L03
       mov       r11d,[rax]
       test      r11d,0FF80FF80
       jne       near ptr M02_L19
       add       rax,4
M02_L03:
       test      r10b,1
       je        short M02_L05
       cmp       word ptr [rax],7F
       ja        short M02_L05
M02_L04:
       add       rax,2
M02_L05:
       sub       rax,rcx
       shr       rax,1
       mov       r10d,eax
       lea       rcx,[rcx+r10*2]
       sub       edx,eax
       je        near ptr M02_L21
       xor       eax,eax
       xor       r10d,r10d
       mov       r11d,edx
       lea       r11,[rcx+r11*2]
       cmp       edx,8
       jl        short M02_L08
       vbroadcastss xmm0,dword ptr [7FFC5439D200]
       vbroadcastss xmm1,dword ptr [7FFC5439D204]
       vbroadcastss xmm2,dword ptr [7FFC5439D208]
       lea       rdx,[r11-10]
M02_L06:
       vmovups   xmm3,[rcx]
       add       rcx,10
       vpminuw   xmm4,xmm3,xmm0
       vpaddusw  xmm5,xmm3,xmm1
       vpor      xmm4,xmm5,xmm4
       vpmovmskb ebx,xmm4
       popcnt    ebx,ebx
       vpaddw    xmm4,xmm3,xmm2
       vpcmpgtw  xmm4,xmm1,xmm4
       vpmovmskb esi,xmm4
M02_L07:
       cmp       esi,0FFFF
       jne       near ptr M02_L22
       add       rax,rbx
       cmp       rcx,rdx
       jbe       short M02_L06
M02_L08:
       cmp       rcx,r11
       jae       short M02_L11
       nop       dword ptr [rax]
M02_L09:
       movzx     edx,word ptr [rcx]
       cmp       edx,7F
       jbe       short M02_L10
       lea       ebx,[rdx+1F800]
       shr       ebx,10
       add       rax,rbx
       add       edx,0FFFF2800
       cmp       edx,7FF
       jbe       near ptr M02_L24
M02_L10:
       add       rcx,2
       cmp       rcx,r11
       jb        short M02_L09
M02_L11:
       mov       [r8],rax
       mov       [r9],r10d
M02_L12:
       mov       rax,rcx
       vzeroupper
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M02_L13:
       vbroadcastss xmm0,dword ptr [7FFC5439D20C]
       vptest    xmm0,xmmword ptr [rax]
       jne       near ptr M02_L00
       lea       rbx,[r11+r10*2-10]
       add       r11,10
       mov       rax,r11
       and       rax,0FFFFFFFFFFFFFFF0
       vpand     xmm1,xmm0,[rax]
       vptest    xmm1,xmm1
       jne       short M02_L17
M02_L14:
       add       rax,10
       cmp       rax,rbx
       ja        short M02_L17
       vpand     xmm1,xmm0,[rax]
       vptest    xmm1,xmm1
       jne       short M02_L17
       jmp       short M02_L14
M02_L15:
       vbroadcastss ymm0,dword ptr [7FFC5439D20C]
       vptest    ymm0,ymmword ptr [rax]
       jne       near ptr M02_L00
       lea       rbx,[r11+r10*2-20]
       lea       rax,[r11+20]
       and       rax,0FFFFFFFFFFFFFFE0
       vpand     ymm1,ymm0,[rax]
       vptest    ymm1,ymm1
       jne       short M02_L17
M02_L16:
       add       rax,20
       cmp       rax,rbx
       ja        short M02_L17
       vpand     ymm1,ymm0,[rax]
       vptest    ymm1,ymm1
       je        short M02_L16
M02_L17:
       mov       r11,rax
       sub       r11,rcx
       shr       r11,1
       sub       r10,r11
       jmp       near ptr M02_L00
M02_L18:
       test      r11d,0FF80FF80
       je        short M02_L20
M02_L19:
       test      r11d,0FF80
       jne       near ptr M02_L05
       jmp       near ptr M02_L04
M02_L20:
       mov       r11d,ebx
       add       rax,4
       jmp       short M02_L19
M02_L21:
       xor       eax,eax
       mov       [r8],rax
       mov       [r9],eax
       jmp       near ptr M02_L12
M02_L22:
       not       esi
       vpsrlw    xmm4,xmm3,3
       vpmovmskb edi,xmm4
       mov       ebp,edi
       and       ebp,esi
       xor       edi,5555
       and       esi,edi
       shl       esi,2
       movzx     edi,si
       cmp       edi,ebp
       jne       short M02_L25
       cmp       esi,0FFFF
       jbe       short M02_L23
       movzx     esi,si
       add       rbx,0FFFFFFFFFFFFFFFE
       add       rcx,0FFFFFFFFFFFFFFFE
M02_L23:
       popcnt    esi,esi
       sub       r10d,esi
       sub       rax,rsi
       sub       rax,rsi
       mov       esi,0FFFF
       jmp       near ptr M02_L07
M02_L24:
       add       rax,0FFFFFFFFFFFFFFFE
       mov       rdx,r11
       sub       rdx,rcx
       cmp       rdx,4
       jb        near ptr M02_L11
       mov       edx,[rcx]
       add       edx,23FF2800
       test      edx,0FC00FC00
       jne       near ptr M02_L11
       dec       r10d
       add       rax,2
       add       rcx,2
       jmp       near ptr M02_L10
M02_L25:
       add       rcx,0FFFFFFFFFFFFFFF0
       cmp       rcx,r11
       jae       near ptr M02_L11
       jmp       near ptr M02_L09
; Total bytes of code 668
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
       jg        short M03_L00
       vmovdqu   xmm0,xmmword ptr [rcx+8]
       vmovdqu   xmmword ptr [rdx],xmm0
       mov       rax,rdx
       add       rsp,28
       ret
M03_L00:
       mov       edx,[rcx+14]
       mov       ecx,r8d
       call      qword ptr [7FFC5468FF30]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 48
```
```asm
; Bshox.Internals.EncodingHelper.Utf8Encode(System.ReadOnlySpan`1<Char>, Byte ByRef, Int32)
;     {
;     ^
;         fixed (char* charsPtr = &MemoryMarshal.GetReference(chars))
;                ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         fixed (byte* bytesPtr = &bytes)
;                ^^^^^^^^^^^^^^^^^^^^^^^
;             return Utf8NoBom.GetBytes(charsPtr, chars.Length, bytesPtr, byteCount);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       sub       rsp,38
       mov       r9,[rcx]
       mov       [rsp+30],r9
       mov       rax,r9
       mov       [rsp+28],rdx
       mov       r9d,[rcx+8]
       mov       [rsp+20],r8d
       mov       r8d,r9d
       mov       r9,rdx
       mov       rcx,2648E0012F8
       mov       rcx,[rcx]
       mov       rdx,rax
       call      qword ptr [7FFC5442CCA8]; System.Text.UTF8Encoding.GetBytes(Char*, Int32, Byte*, Int32)
       nop
       add       rsp,38
       ret
; Total bytes of code 63
```

## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: DefaultJob)

```asm
; Benchmark.WriteString.Ascii()
;         var writer = new BshoxWriter(fixedBufferWriter);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         for (int i = 0; i < Count; i++)
;              ^^^^^^^^^
;             writer.WriteString(_stringsAscii[i]);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         fixedBufferWriter.Reset();
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^
;         return writer.UnflushedBytes;
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       push      rbp
       push      r15
       push      r14
       push      r13
       push      r12
       push      rdi
       push      rsi
       push      rbx
       sub       rsp,178
       lea       rbp,[rsp+1B0]
       xor       eax,eax
       mov       [rbp-148],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rbp-140],xmm4
       mov       rax,0FFFFFFFFFFFFFF10
M00_L00:
       vmovdqa   xmmword ptr [rbp+rax-40],xmm4
       vmovdqa   xmmword ptr [rbp+rax-30],xmm4
       vmovdqa   xmmword ptr [rbp+rax-20],xmm4
       add       rax,30
       jne       short M00_L00
       mov       rsi,rcx
       mov       rdi,[rsi+18]
       xor       ebx,ebx
       xor       r14d,r14d
       xor       r15d,r15d
       mov       r8,16B7F8012F0
       mov       r13,[r8]
       xor       r12d,r12d
M00_L01:
       mov       r8,[rsi+10]
       cmp       r12d,[r8+8]
       jae       near ptr M00_L49
       mov       rax,[r8+r12*8+10]
       mov       [rbp-158],rax
       mov       r10d,[rax+8]
       mov       [rbp-14C],r10d
       test      r10d,r10d
       je        near ptr M00_L26
       cmp       r10d,2A
       jle       near ptr M00_L31
       lea       r8,[rax+0C]
       mov       [rbp-70],r8
       mov       r11,[rbp-70]
       mov       [rbp-128],r11
       mov       r8,16B7F8012F8
       mov       rdx,[r8]
       mov       [rbp-170],rdx
       mov       [rbp-160],rdx
       mov       [rbp-74],r10d
       lea       r8,[rbp-80]
       lea       r9,[rbp-88]
       mov       rcx,r11
       mov       edx,[rbp-74]
       call      qword ptr [7FFC54337AE0]; System.Text.Unicode.Utf16Utility.GetPointerToFirstInvalidChar(Char*, Int32, Int64 ByRef, Int32 ByRef)
       mov       rdx,[rbp-128]
       sub       rax,rdx
       mov       r9,rax
       shr       r9,3F
       add       r9,rax
       sar       r9,1
       movsxd    rax,r9d
       add       rax,[rbp-80]
       cmp       rax,7FFFFFFF
       ja        near ptr M00_L44
       mov       [rbp-78],eax
       mov       r8d,[rbp-74]
       cmp       r9d,r8d
       jne       near ptr M00_L36
M00_L02:
       xor       ecx,ecx
       mov       [rbp-70],rcx
       mov       eax,[rbp-78]
       mov       [rbp-3C],eax
       mov       [rbp-8C],eax
       cmp       r14d,5
       jl        near ptr M00_L08
M00_L03:
       xor       ecx,ecx
       mov       r8d,[rbp-8C]
       cmp       r8d,7F
       ja        near ptr M00_L39
M00_L04:
       movsxd    rdx,ecx
       mov       [rbx+rdx],r8b
       inc       ecx
       mov       [rbp-0C4],ecx
       test      ecx,ecx
       jl        near ptr M00_L45
       mov       edx,ecx
       add       rbx,rdx
       sub       r14d,ecx
       add       r15d,ecx
       mov       eax,[rbp-3C]
       cmp       r14d,eax
       jl        near ptr M00_L16
M00_L05:
       mov       r10,[rbp-158]
       add       r10,0C
       mov       r11d,[rbp-14C]
       mov       [rbp-11C],r11d
       xor       ecx,ecx
       mov       [rbp-100],rcx
       mov       [rbp-108],rcx
       mov       [rbp-100],r10
       mov       [rbp-130],r10
       mov       [rbp-108],rbx
       mov       r9,rbx
       mov       [rbp-138],r9
       test      r10,r10
       je        near ptr M00_L24
       test      r9,r9
       je        near ptr M00_L46
       mov       eax,[rbp-3C]
       mov       ecx,r11d
       or        ecx,eax
       jl        near ptr M00_L47
       mov       r8,[rbp-170]
       mov       [rbp-168],r8
       lea       rcx,[rbp-110]
       mov       [rsp+20],rcx
       lea       rcx,[rbp-118]
       mov       [rsp+28],rcx
       mov       rcx,r10
       mov       edx,r11d
       mov       r8,r9
       mov       r9d,eax
       call      qword ptr [7FFC54337C00]; System.Text.Unicode.Utf8Utility.TranscodeToUtf8(Char*, Int32, Byte*, Int32, Char* ByRef, Byte* ByRef)
       mov       rcx,[rbp-110]
       mov       rdx,[rbp-130]
       sub       rcx,rdx
       mov       r8,rcx
       shr       r8,3F
       add       rcx,r8
       sar       rcx,1
       mov       r8d,[rbp-118]
       mov       r9,[rbp-138]
       sub       r8d,r9d
       mov       r11d,[rbp-11C]
       cmp       ecx,r11d
       jne       near ptr M00_L42
M00_L06:
       xor       ecx,ecx
       mov       [rbp-100],rcx
       mov       [rbp-108],rcx
       mov       ecx,[rbp-3C]
       test      ecx,ecx
       jl        near ptr M00_L48
       mov       edx,ecx
       add       rbx,rdx
       sub       r14d,ecx
       add       r15d,ecx
M00_L07:
       inc       r12d
       cmp       r12d,3E8
       jl        near ptr M00_L01
       mov       rdi,[rsi+18]
       lea       rsi,[rdi+18]
       add       rdi,8
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,r15d
       add       rsp,178
       pop       rbx
       pop       rsi
       pop       rdi
       pop       r12
       pop       r13
       pop       r14
       pop       r15
       pop       rbp
       ret
M00_L08:
       test      r15d,r15d
       jg        near ptr M00_L37
M00_L09:
       mov       edx,[r13+0C]
       cmp       edx,5
       jg        short M00_L10
       mov       r10d,5
       jmp       short M00_L11
M00_L10:
       mov       r10d,edx
M00_L11:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rdi],rdx
       jne       near ptr M00_L38
       lea       rdx,[rbp-0B0]
       mov       rcx,rdi
       mov       r8d,r10d
       call      qword ptr [7FFC54596160]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       ebx,ebx
       xor       r14d,r14d
       mov       rcx,[rbp-0B0]
       test      rcx,rcx
       je        short M00_L14
       mov       rax,[rcx]
       mov       rdx,rax
       test      dword ptr [rdx],80000000
       jne       short M00_L12
       lea       rdx,[rbp-0C0]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       r14d,[rbp-0B8]
       mov       rbx,[rbp-0C0]
       jmp       short M00_L13
M00_L12:
       lea       rbx,[rcx+10]
       mov       r14d,[rcx+8]
M00_L13:
       mov       edx,[rbp-0A8]
       and       edx,7FFFFFFF
       mov       ecx,[rbp-0A4]
       mov       r8d,ecx
       add       r8,rdx
       mov       r11d,r14d
       cmp       r8,r11
       ja        near ptr M00_L23
       add       rbx,rdx
       mov       r14d,ecx
M00_L14:
       mov       [rbp-0A0],rbx
       mov       [rbp-98],r14d
M00_L15:
       cmp       dword ptr [rbp-98],0
       jbe       near ptr M00_L49
       mov       rbx,[rbp-0A0]
       mov       r14d,[rbp-98]
       jmp       near ptr M00_L03
M00_L16:
       test      r15d,r15d
       jg        near ptr M00_L40
M00_L17:
       mov       r8d,[r13+0C]
       mov       eax,[rbp-3C]
       cmp       eax,r8d
       jl        short M00_L18
       mov       r8d,eax
       mov       eax,[rbp-3C]
M00_L18:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rdi],rdx
       jne       near ptr M00_L41
       lea       rdx,[rbp-0E8]
       mov       rcx,rdi
       call      qword ptr [7FFC54596160]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       ebx,ebx
       xor       r14d,r14d
       mov       rcx,[rbp-0E8]
       test      rcx,rcx
       je        short M00_L21
       mov       rax,[rcx]
       mov       rdx,rax
       test      dword ptr [rdx],80000000
       jne       short M00_L19
       lea       rdx,[rbp-0F8]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       r14d,[rbp-0F0]
       mov       rbx,[rbp-0F8]
       jmp       short M00_L20
M00_L19:
       lea       rbx,[rcx+10]
       mov       r14d,[rcx+8]
M00_L20:
       mov       edx,[rbp-0E0]
       and       edx,7FFFFFFF
       mov       ecx,[rbp-0DC]
       mov       r8d,ecx
       add       r8,rdx
       mov       r11d,r14d
       cmp       r8,r11
       ja        short M00_L23
       add       rbx,rdx
       mov       r14d,ecx
M00_L21:
       mov       rdx,rbx
       mov       [rbp-0D8],rdx
       mov       [rbp-0D0],r14d
M00_L22:
       cmp       dword ptr [rbp-0D0],0
       jbe       near ptr M00_L49
       mov       rbx,[rbp-0D8]
       mov       r14d,[rbp-0D0]
       jmp       near ptr M00_L05
M00_L23:
       call      qword ptr [7FFC54507990]
       int       3
M00_L24:
       mov       ecx,0E
M00_L25:
       mov       edx,31
       call      qword ptr [7FFC54714168]
       int       3
M00_L26:
       test      r14d,r14d
       jg        short M00_L30
       test      r15d,r15d
       jle       short M00_L27
       mov       rcx,rdi
       mov       edx,r15d
       mov       r11,7FFC54280588
       call      qword ptr [r11]
       xor       r15d,r15d
M00_L27:
       mov       r8d,[r13+0C]
       cmp       r8d,1
       jle       short M00_L28
       jmp       short M00_L29
M00_L28:
       mov       r8d,1
M00_L29:
       lea       rdx,[rbp-50]
       mov       rcx,rdi
       mov       r11,7FFC54280580
       call      qword ptr [r11]
       cmp       dword ptr [rbp-48],0
       jbe       near ptr M00_L49
       mov       rbx,[rbp-50]
       mov       r14d,[rbp-48]
M00_L30:
       mov       byte ptr [rbx],0
       inc       rbx
       dec       r14d
       inc       r15d
       jmp       near ptr M00_L07
M00_L31:
       cmp       r14d,80
       jge       short M00_L35
       test      r15d,r15d
       jle       short M00_L32
       mov       rcx,rdi
       mov       edx,r15d
       mov       r11,7FFC54280598
       call      qword ptr [r11]
       xor       r15d,r15d
       mov       rax,[rbp-158]
       mov       r10d,[rbp-14C]
M00_L32:
       mov       r8d,[r13+0C]
       cmp       r8d,80
       jle       short M00_L33
       jmp       short M00_L34
M00_L33:
       mov       r8d,80
M00_L34:
       lea       rdx,[rbp-60]
       mov       rcx,rdi
       mov       r11,7FFC54280590
       call      qword ptr [r11]
       cmp       dword ptr [rbp-58],0
       jbe       near ptr M00_L49
       mov       rbx,[rbp-60]
       mov       r14d,[rbp-58]
       mov       rax,[rbp-158]
       mov       r10d,[rbp-14C]
M00_L35:
       lea       rcx,[rax+0C]
       mov       edx,r10d
       mov       [rbp-148],rcx
       mov       [rbp-140],edx
       lea       rcx,[rbp-148]
       lea       rdx,[rbx+1]
       mov       r8d,7F
       call      qword ptr [7FFC5469FDE0]; Bshox.Internals.EncodingHelper.Utf8Encode(System.ReadOnlySpan`1<Char>, Byte ByRef, Int32)
       mov       [rbx],al
       lea       ecx,[rax+1]
       mov       [rbp-64],ecx
       test      ecx,ecx
       jl        near ptr M00_L43
       mov       edx,ecx
       add       rbx,rdx
       sub       r14d,ecx
       add       r15d,ecx
       jmp       near ptr M00_L07
M00_L36:
       mov       rcx,[rbp-160]
       call      qword ptr [7FFC54714150]
       add       eax,[rbp-78]
       mov       ecx,eax
       test      ecx,ecx
       mov       [rbp-78],ecx
       jge       near ptr M00_L02
       jmp       near ptr M00_L44
M00_L37:
       mov       rcx,rdi
       mov       edx,r15d
       mov       r11,7FFC542805A8
       call      qword ptr [r11]
       xor       r15d,r15d
       jmp       near ptr M00_L09
M00_L38:
       lea       rdx,[rbp-0A0]
       mov       rcx,rdi
       mov       r8d,r10d
       mov       r11,7FFC542805A0
       call      qword ptr [r11]
       jmp       near ptr M00_L15
M00_L39:
       lea       edx,[rcx+1]
       mov       r11d,r8d
       or        r11d,0FFFFFF80
       movsxd    rcx,ecx
       mov       [rbx+rcx],r11b
       shr       r8d,7
       cmp       r8d,7F
       mov       ecx,edx
       ja        short M00_L39
       jmp       near ptr M00_L04
M00_L40:
       mov       rcx,rdi
       mov       edx,r15d
       mov       r11,7FFC542805B8
       call      qword ptr [r11]
       xor       r15d,r15d
       jmp       near ptr M00_L17
M00_L41:
       lea       rdx,[rbp-0D8]
       mov       rcx,rdi
       mov       r11,7FFC542805B0
       call      qword ptr [r11]
       jmp       near ptr M00_L22
M00_L42:
       mov       eax,[rbp-3C]
       mov       [rsp+20],eax
       mov       [rsp+28],ecx
       mov       [rsp+30],r8d
       mov       dword ptr [rsp+38],1
       mov       rcx,[rbp-168]
       mov       r8d,r11d
       call      qword ptr [7FFC54714138]
       jmp       near ptr M00_L06
M00_L43:
       mov       ecx,225
       mov       rdx,7FFC546F42E8
       call      qword ptr [7FFC5433EE38]
       mov       rdx,rax
       mov       ecx,[rbp-64]
       call      qword ptr [7FFC54714108]
       int       3
M00_L44:
       call      qword ptr [7FFC54714180]
       int       3
M00_L45:
       mov       ecx,225
       mov       rdx,7FFC546F42E8
       call      qword ptr [7FFC5433EE38]
       mov       rdx,rax
       mov       ecx,[rbp-0C4]
       call      qword ptr [7FFC54714108]
       int       3
M00_L46:
       mov       ecx,0A
       jmp       near ptr M00_L25
M00_L47:
       mov       ecx,0C
       mov       edx,0D
       call      qword ptr [7FFC54695770]
       int       3
M00_L48:
       mov       ecx,225
       mov       rdx,7FFC546F42E8
       call      qword ptr [7FFC5433EE38]
       mov       rdx,rax
       mov       ecx,[rbp-3C]
       call      qword ptr [7FFC54714108]
       int       3
M00_L49:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 1852
```
```asm
; System.Text.Unicode.Utf16Utility.GetPointerToFirstInvalidChar(Char*, Int32, Int64 ByRef, Int32 ByRef)
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       mov       rax,rcx
       mov       r10d,edx
       mov       r11,rax
       cmp       r10,20
       jae       near ptr M01_L09
       cmp       r10,10
       jae       near ptr M01_L07
M01_L00:
       cmp       r10,4
       jb        short M01_L02
M01_L01:
       mov       r11d,[rax]
       mov       ebx,[rax+4]
       mov       esi,r11d
       or        esi,ebx
       test      esi,0FF80FF80
       jne       near ptr M01_L12
       add       rax,8
       add       r10,0FFFFFFFFFFFFFFFC
       cmp       r10,4
       jae       short M01_L01
M01_L02:
       test      r10b,2
       je        short M01_L03
       mov       r11d,[rax]
       test      r11d,0FF80FF80
       jne       near ptr M01_L13
       add       rax,4
M01_L03:
       test      r10b,1
       je        short M01_L05
       cmp       word ptr [rax],7F
       ja        short M01_L05
M01_L04:
       add       rax,2
M01_L05:
       sub       rax,rcx
       shr       rax,1
       mov       r10d,eax
       lea       rcx,[rcx+r10*2]
       sub       edx,eax
       jne       near ptr M01_L15
       xor       edx,edx
       mov       [r8],rdx
       mov       [r9],edx
M01_L06:
       mov       rax,rcx
       vzeroupper
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M01_L07:
       vbroadcastss xmm0,dword ptr [7FFC543AD1E0]
       vptest    xmm0,xmmword ptr [rax]
       jne       near ptr M01_L00
       lea       rbx,[r11+r10*2-10]
       add       r11,10
       mov       rax,r11
       and       rax,0FFFFFFFFFFFFFFF0
       vpand     xmm1,xmm0,[rax]
       vptest    xmm1,xmm1
       jne       short M01_L11
M01_L08:
       add       rax,10
       cmp       rax,rbx
       ja        short M01_L11
       vpand     xmm1,xmm0,[rax]
       vptest    xmm1,xmm1
       jne       short M01_L11
       jmp       short M01_L08
M01_L09:
       vbroadcastss ymm0,dword ptr [7FFC543AD1E0]
       vptest    ymm0,ymmword ptr [rax]
       jne       near ptr M01_L00
       lea       rbx,[r11+r10*2-20]
       lea       rax,[r11+20]
       and       rax,0FFFFFFFFFFFFFFE0
       vpand     ymm1,ymm0,[rax]
       vptest    ymm1,ymm1
       jne       short M01_L11
M01_L10:
       add       rax,20
       cmp       rax,rbx
       ja        short M01_L11
       vpand     ymm1,ymm0,[rax]
       vptest    ymm1,ymm1
       je        short M01_L10
M01_L11:
       mov       r11,rax
       sub       r11,rcx
       shr       r11,1
       sub       r10,r11
       jmp       near ptr M01_L00
M01_L12:
       test      r11d,0FF80FF80
       je        short M01_L14
M01_L13:
       test      r11d,0FF80
       jne       near ptr M01_L05
       jmp       near ptr M01_L04
M01_L14:
       mov       r11d,ebx
       add       rax,4
       jmp       short M01_L13
M01_L15:
       xor       eax,eax
       xor       r10d,r10d
       mov       r11d,edx
       lea       r11,[rcx+r11*2]
       cmp       edx,8
       jl        near ptr M01_L20
       vbroadcastss xmm0,dword ptr [7FFC543AD1E4]
       lea       rdx,[r11-10]
M01_L16:
       vmovups   xmm1,[rcx]
       add       rcx,10
       vpaddusw  xmm2,xmm1,xmm0
       vpminuw   xmm3,xmm1,[7FFC543AD1F0]
       vpor      xmm2,xmm2,xmm3
       vpmovmskb ebx,xmm2
       popcnt    ebx,ebx
       vpaddw    xmm2,xmm1,[7FFC543AD200]
       vpcmpgtw  xmm2,xmm0,xmm2
       vpmovmskb esi,xmm2
M01_L17:
       cmp       esi,0FFFF
       je        short M01_L19
       not       esi
       vpsrlw    xmm2,xmm1,3
       vpmovmskb edi,xmm2
       mov       ebp,edi
       and       ebp,esi
       xor       edi,5555
       and       esi,edi
       shl       esi,2
       movzx     edi,si
       cmp       edi,ebp
       jne       near ptr M01_L22
       cmp       esi,0FFFF
       jbe       short M01_L18
       movzx     esi,si
       add       rbx,0FFFFFFFFFFFFFFFE
       add       rcx,0FFFFFFFFFFFFFFFE
M01_L18:
       popcnt    esi,esi
       sub       r10d,esi
       sub       rax,rsi
       sub       rax,rsi
       mov       esi,0FFFF
       jmp       short M01_L17
M01_L19:
       add       rax,rbx
       cmp       rcx,rdx
       jbe       near ptr M01_L16
M01_L20:
       cmp       rcx,r11
       jae       short M01_L23
       movzx     edx,word ptr [rcx]
       cmp       edx,7F
       jbe       short M01_L21
       lea       ebx,[rdx+1F800]
       shr       ebx,10
       add       rax,rbx
       add       edx,0FFFF2800
       cmp       edx,7FF
       ja        short M01_L21
       add       rax,0FFFFFFFFFFFFFFFE
       mov       rdx,r11
       sub       rdx,rcx
       cmp       rdx,4
       jb        short M01_L23
       mov       edx,[rcx]
       add       edx,23FF2800
       test      edx,0FC00FC00
       jne       short M01_L23
       dec       r10d
       add       rax,2
       add       rcx,2
M01_L21:
       add       rcx,2
       jmp       short M01_L20
M01_L22:
       add       rcx,0FFFFFFFFFFFFFFF0
       jmp       short M01_L20
M01_L23:
       mov       [r8],rax
       mov       [r9],r10d
       jmp       near ptr M01_L06
; Total bytes of code 628
```
```asm
; System.Text.Unicode.Utf8Utility.TranscodeToUtf8(Char*, Int32, Byte*, Int32, Char* ByRef, Byte* ByRef)
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       mov       r10,[rsp+48]
       mov       r11,[rsp+50]
       cmp       edx,r9d
       mov       eax,r9d
       cmovle    eax,edx
       xor       ebx,ebx
       cmp       rax,20
       jb        near ptr M02_L03
       mov       rsi,[rcx]
       mov       rdi,0FF80FF80FF80FF80
       test      rdi,rsi
       mov       rdi,rsi
       jne       near ptr M02_L18
       cmp       rax,40
       jb        near ptr M02_L11
       mov       rbx,rcx
       vmovups   ymm0,[rbx]
       vbroadcastss ymm1,dword ptr [7FFC543AECC0]
       vptest    ymm1,ymm0
       jne       near ptr M02_L09
       mov       rsi,r8
       vpackuswb ymm0,ymm0,ymm0
       vpermq    ymm0,ymm0,0D8
       vmovups   [rsi],xmm0
       mov       edi,10
       test      r8b,10
       jne       short M02_L00
       vmovups   ymm0,[rbx+20]
       vptest    ymm1,ymm0
       jne       short M02_L02
       vpackuswb ymm0,ymm0,ymm0
       vpermq    ymm0,ymm0,0D8
       vmovups   [rsi+10],xmm0
M02_L00:
       mov       rdi,r8
       and       rdi,1F
       neg       rdi
       add       rdi,20
       lea       rbp,[rax-20]
       vmovups   ymm0,[rbx+rdi*2]
       vmovups   ymm2,[rbx+rdi*2+20]
       vpor      ymm3,ymm0,ymm2
       vptest    ymm3,ymm1
       jne       near ptr M02_L10
M02_L01:
       vpackuswb ymm0,ymm0,ymm2
       vpermq    ymm2,ymm0,0D8
       vmovups   [rsi+rdi],ymm2
       add       rdi,20
       cmp       rdi,rbp
       ja        short M02_L02
       vmovups   ymm0,[rbx+rdi*2]
       vmovups   ymm2,[rbx+rdi*2+20]
       vpor      ymm3,ymm0,ymm2
       vptest    ymm3,ymm1
       jne       near ptr M02_L10
       jmp       short M02_L01
M02_L02:
       mov       rbx,rdi
M02_L03:
       sub       rax,rbx
       cmp       rax,4
       jb        short M02_L05
       lea       rsi,[rbx+rax-4]
       mov       rdi,[rcx+rbx*2]
       mov       rbp,0FF80FF80FF80FF80
       test      rbp,rdi
       jne       near ptr M02_L18
M02_L04:
       vmovq     xmm0,rdi
       vpackuswb xmm0,xmm0,xmm0
       vmovd     dword ptr [r8+rbx],xmm0
       add       rbx,4
       cmp       rbx,rsi
       ja        short M02_L05
       mov       rdi,[rcx+rbx*2]
       mov       rbp,0FF80FF80FF80FF80
       test      rbp,rdi
       jne       near ptr M02_L18
       jmp       short M02_L04
M02_L05:
       test      al,2
       je        short M02_L06
       mov       esi,[rcx+rbx*2]
       test      esi,0FF80FF80
       jne       near ptr M02_L19
       lea       rdi,[r8+rbx]
       mov       [rdi],sil
       shr       esi,10
       mov       [rdi+1],sil
       add       rbx,2
M02_L06:
       test      al,1
       je        short M02_L08
       movzx     esi,word ptr [rcx+rbx*2]
       cmp       esi,7F
       ja        short M02_L08
M02_L07:
       mov       [r8+rbx],sil
       inc       rbx
M02_L08:
       lea       rcx,[rcx+rbx*2]
       add       r8,rbx
       cmp       ebx,edx
       jne       near ptr M02_L20
       mov       [r10],rcx
       mov       [r11],r8
       xor       eax,eax
       vzeroupper
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M02_L09:
       xor       ebx,ebx
       jmp       near ptr M02_L03
M02_L10:
       vptest    ymm1,ymm0
       jne       near ptr M02_L02
       vpackuswb ymm0,ymm0,ymm0
       vpermq    ymm0,ymm0,0D8
       vmovups   [rsi+rdi],xmm0
       add       rdi,10
       jmp       near ptr M02_L02
M02_L11:
       mov       rbx,rcx
       vmovups   xmm0,[rbx]
       vptest    xmm0,xmmword ptr [7FFC543AECD0]
       je        short M02_L12
       xor       ebx,ebx
       jmp       near ptr M02_L17
M02_L12:
       mov       rsi,r8
       vpackuswb xmm0,xmm0,xmm0
       vmovsd    qword ptr [rsi],xmm0
       mov       edi,8
       test      r8b,8
       jne       short M02_L13
       vmovups   xmm0,[rbx+10]
       vptest    xmm0,xmmword ptr [7FFC543AECD0]
       jne       short M02_L15
       vpackuswb xmm0,xmm0,xmm0
       vmovsd    qword ptr [rsi+8],xmm0
M02_L13:
       mov       rdi,r8
       and       rdi,0F
       neg       rdi
       add       rdi,10
       lea       rbp,[rax-10]
M02_L14:
       vmovups   xmm0,[rbx+rdi*2]
       vmovups   xmm1,[rbx+rdi*2+10]
       vpor      xmm2,xmm0,xmm1
       vptest    xmm2,xmmword ptr [7FFC543AECD0]
       jne       short M02_L16
       vpackuswb xmm0,xmm0,xmm1
       vmovups   [rsi+rdi],xmm0
       add       rdi,10
       cmp       rdi,rbp
       jbe       short M02_L14
M02_L15:
       mov       rbx,rdi
       jmp       short M02_L17
M02_L16:
       vptest    xmm0,xmmword ptr [7FFC543AECD0]
       jne       short M02_L15
       vpackuswb xmm0,xmm0,xmm0
       vmovsd    qword ptr [rsi+rdi],xmm0
       add       rdi,8
       jmp       short M02_L15
M02_L17:
       jmp       near ptr M02_L03
M02_L18:
       mov       eax,edi
       test      eax,0FF80FF80
       jne       short M02_L22
       lea       rsi,[r8+rbx]
       mov       [rsi],al
       shr       eax,10
       mov       [rsi+1],al
       shr       rdi,20
       mov       eax,edi
       add       rbx,2
       mov       esi,eax
M02_L19:
       test      esi,0FF80
       je        near ptr M02_L07
       jmp       near ptr M02_L08
M02_L20:
       sub       edx,ebx
       sub       r9d,ebx
       cmp       edx,2
       jl        near ptr M02_L58
       mov       eax,edx
       lea       rax,[rcx+rax*2-4]
M02_L21:
       mov       ebx,[rcx]
       jmp       near ptr M02_L47
M02_L22:
       mov       esi,eax
       jmp       short M02_L19
M02_L23:
       cmp       r9d,2
       jl        near ptr M02_L59
       mov       esi,ebx
       shr       esi,8
       or        esi,ebx
       mov       [r8],si
       add       rcx,4
       add       r8,2
       add       r9d,0FFFFFFFE
       mov       rbx,rax
       sub       rbx,rcx
       mov       rsi,rbx
       shr       rsi,3F
       add       rbx,rsi
       sar       rbx,1
       add       ebx,2
       movsxd    rsi,r9d
       cmp       rbx,rsi
       jle       short M02_L24
       jmp       short M02_L25
M02_L24:
       mov       rsi,rbx
M02_L25:
       mov       ebx,esi
       shr       ebx,3
       xor       edi,edi
       jmp       short M02_L27
M02_L26:
       vmovups   xmm0,[rcx]
       vptest    xmm0,xmmword ptr [7FFC543AECD0]
       jne       short M02_L28
       vpackuswb xmm0,xmm0,xmm0
       vmovq     qword ptr [r8],xmm0
       add       rcx,10
       add       r8,8
       inc       edi
M02_L27:
       cmp       edi,ebx
       jb        short M02_L26
       lea       ebx,[rdi*8]
       sub       r9d,ebx
       test      sil,4
       je        near ptr M02_L55
       mov       rbx,[rcx]
       mov       rsi,0FF80FF80FF80FF80
       test      rsi,rbx
       jne       short M02_L29
       jmp       near ptr M02_L50
M02_L28:
       shl       edi,3
       sub       r9d,edi
       vmovq     rbx,xmm0
       mov       rsi,0FF80FF80FF80FF80
       test      rsi,rbx
       jne       short M02_L29
       vpackuswb xmm1,xmm0,xmm0
       vmovd     dword ptr [r8],xmm1
       add       rcx,8
       add       r8,4
       add       r9d,0FFFFFFFC
       vpextrq   rbx,xmm0,1
M02_L29:
       mov       esi,ebx
       test      esi,0FF80FF80
       jne       short M02_L30
       mov       edi,esi
       shr       edi,8
       or        edi,esi
       mov       [r8],di
       add       rcx,4
       add       r8,2
       add       r9d,0FFFFFFFE
       shr       rbx,20
       mov       esi,ebx
M02_L30:
       test      esi,0FF80
       jne       short M02_L31
       test      r9d,r9d
       je        near ptr M02_L66
       jmp       short M02_L32
M02_L31:
       test      esi,0F800
       jne       near ptr M02_L48
       jmp       near ptr M02_L36
M02_L32:
       mov       [r8],sil
       add       rcx,2
       inc       r8
       dec       r9d
       cmp       rcx,rax
       ja        near ptr M02_L57
       mov       esi,[rcx]
       jmp       short M02_L31
M02_L33:
       cmp       r9d,2
       jl        near ptr M02_L66
       jmp       short M02_L37
M02_L34:
       cmp       r9d,4
       jl        short M02_L35
       mov       ebx,esi
       shr       ebx,6
       and       ebx,1F001F
       shl       esi,8
       and       esi,3F003F00
       add       ebx,esi
       add       ebx,80C080C0
       mov       [r8],ebx
       add       rcx,4
       add       r8,4
       add       r9d,0FFFFFFFC
       cmp       rcx,rax
       ja        near ptr M02_L57
       mov       esi,[rcx]
       lea       ebx,[rsi-80]
       movzx     ebx,bx
       cmp       ebx,780
       jl        short M02_L36
       mov       ebx,esi
       jmp       near ptr M02_L47
M02_L35:
       mov       ebx,esi
       jmp       near ptr M02_L59
M02_L36:
       lea       ebx,[rsi-800000]
       cmp       ebx,77FFFFF
       jbe       short M02_L34
       jmp       short M02_L33
M02_L37:
       lea       ebx,[rsi*4]
       and       ebx,1F00
       mov       edi,esi
       and       edi,3F
       lea       ebx,[rbx+rdi+0C080]
       movbe     [r8],bx
       cmp       esi,800000
       jb        short M02_L38
       add       rcx,2
       add       r8,2
       add       r9d,0FFFFFFFE
       cmp       rcx,rax
       ja        near ptr M02_L57
       jmp       short M02_L39
M02_L38:
       cmp       r9d,3
       jl        near ptr M02_L56
       jmp       near ptr M02_L51
M02_L39:
       mov       esi,[rcx]
       jmp       near ptr M02_L48
M02_L40:
       test      esi,0F8000000
       jne       short M02_L42
       jmp       short M02_L43
M02_L41:
       lea       ebx,[rsi+23FF2800]
       test      ebx,0FC00FC00
       je        near ptr M02_L53
       jmp       near ptr M02_L52
M02_L42:
       lea       ebx,[rsi+28000000]
       cmp       ebx,8000000
       jb        short M02_L43
       cmp       r9d,6
       jge       short M02_L44
M02_L43:
       cmp       r9d,3
       jl        near ptr M02_L66
       jmp       short M02_L45
M02_L44:
       lea       ebx,[rsi*4]
       and       ebx,3F00
       mov       edi,esi
       and       edi,3F
       shl       edi,10
       or        ebx,edi
       mov       edi,esi
       shr       edi,4
       and       edi,0F000000
       mov       ebp,esi
       shr       ebp,0C
       and       ebp,0F
       or        edi,ebp
       add       ebx,edi
       add       ebx,0E08080E0
       mov       [r8],ebx
       mov       ebx,esi
       shr       ebx,16
       and       ebx,3F
       shr       esi,8
       and       esi,3F00
       add       ebx,esi
       add       ebx,8080
       mov       [r8+4],bx
       add       rcx,4
       add       r8,6
       add       r9d,0FFFFFFFA
       cmp       rcx,rax
       ja        near ptr M02_L57
       mov       esi,[rcx]
       test      esi,0F800
       jne       near ptr M02_L48
       mov       ebx,esi
       jmp       short M02_L47
M02_L45:
       lea       ebx,[rsi*4]
       and       ebx,3F00
       movzx     edi,si
       shr       edi,0C
       add       ebx,edi
       add       ebx,80E0
       mov       [r8],bx
       mov       ebx,esi
       and       ebx,3F
       or        ebx,0FFFFFF80
       mov       [r8+2],bl
       add       rcx,2
       add       r8,3
       add       r9d,0FFFFFFFD
       cmp       esi,800000
       jb        short M02_L46
       cmp       rcx,rax
       ja        near ptr M02_L57
       jmp       short M02_L49
M02_L46:
       test      r9d,r9d
       je        near ptr M02_L66
       shr       esi,10
       mov       [r8],sil
       add       rcx,2
       inc       r8
       dec       r9d
       cmp       rcx,rax
       ja        near ptr M02_L57
       mov       esi,[rcx]
       test      esi,0F800
       jne       short M02_L48
       mov       ebx,esi
M02_L47:
       test      ebx,0FF80FF80
       je        near ptr M02_L23
       mov       esi,ebx
       jmp       near ptr M02_L30
M02_L48:
       lea       ebx,[rsi-0D800]
       test      ebx,0F800
       je        near ptr M02_L41
       jmp       near ptr M02_L40
M02_L49:
       mov       esi,[rcx]
       jmp       near ptr M02_L30
M02_L50:
       vmovq     xmm0,rbx
       vpackuswb xmm0,xmm0,xmm0
       vmovd     dword ptr [r8],xmm0
       add       rcx,8
       jmp       short M02_L54
M02_L51:
       shr       esi,10
       mov       [r8+2],sil
       add       rcx,4
       add       r8,3
       add       r9d,0FFFFFFFD
       jmp       short M02_L55
M02_L52:
       mov       eax,3
       jmp       near ptr M02_L67
M02_L53:
       cmp       r9d,4
       jl        near ptr M02_L66
       add       esi,40
       mov       ebx,esi
       and       ebx,3
       shl       ebx,14
       or        ebx,808080F0
       mov       edi,esi
       and       edi,3F0700
       bswap     edi
       rol       edi,10
       or        ebx,edi
       mov       edi,esi
       shr       edi,6
       and       edi,0F0000
       or        ebx,edi
       and       esi,0FC
       shl       esi,6
       or        ebx,esi
       mov       [r8],ebx
       add       rcx,4
M02_L54:
       add       r8,4
       add       r9d,0FFFFFFFC
M02_L55:
       cmp       rcx,rax
       jbe       near ptr M02_L21
       jmp       short M02_L57
M02_L56:
       add       rcx,2
       add       r8,2
       jmp       near ptr M02_L66
M02_L57:
       sub       rax,rcx
       mov       rdx,rax
       shr       rdx,3F
       add       rdx,rax
       sar       rdx,1
       add       edx,2
M02_L58:
       test      edx,edx
       je        near ptr M02_L65
       movzx     ebx,word ptr [rcx]
       jmp       short M02_L60
M02_L59:
       movzx     ebx,bx
M02_L60:
       cmp       ebx,7F
       ja        short M02_L61
       test      r9d,r9d
       je        near ptr M02_L66
       mov       [r8],bl
       add       rcx,2
       inc       r8
       jmp       near ptr M02_L64
M02_L61:
       cmp       ebx,800
       jae       short M02_L62
       cmp       r9d,2
       jl        near ptr M02_L66
       mov       r9d,ebx
       and       r9d,3F
       or        r9d,0FFFFFF80
       mov       [r8+1],r9b
       shr       ebx,6
       or        ebx,0FFFFFFC0
       mov       [r8],bl
       add       rcx,2
       add       r8,2
       jmp       short M02_L64
M02_L62:
       lea       eax,[rbx-0D800]
       cmp       eax,7FF
       jbe       short M02_L63
       cmp       r9d,3
       jl        short M02_L66
       mov       eax,ebx
       and       eax,3F
       or        eax,0FFFFFF80
       mov       [r8+2],al
       mov       eax,ebx
       shr       eax,6
       and       eax,3F
       or        eax,0FFFFFF80
       mov       [r8+1],al
       mov       eax,ebx
       shr       eax,0C
       or        eax,0FFFFFFE0
       mov       [r8],al
       add       rcx,2
       add       r8,3
       jmp       short M02_L64
M02_L63:
       cmp       ebx,0DBFF
       ja        near ptr M02_L52
       mov       eax,2
       jmp       short M02_L67
M02_L64:
       cmp       edx,1
       jg        short M02_L66
M02_L65:
       xor       eax,eax
       jmp       short M02_L67
M02_L66:
       mov       eax,1
M02_L67:
       mov       [r10],rcx
       mov       [r11],r8
       vzeroupper
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
; Total bytes of code 1975
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
       jg        short M03_L00
       vmovdqu   xmm0,xmmword ptr [rcx+8]
       vmovdqu   xmmword ptr [rdx],xmm0
       mov       rax,rdx
       add       rsp,28
       ret
M03_L00:
       mov       edx,[rcx+14]
       mov       ecx,r8d
       call      qword ptr [7FFC5469FF18]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 48
```
```asm
; Bshox.Internals.EncodingHelper.Utf8Encode(System.ReadOnlySpan`1<Char>, Byte ByRef, Int32)
;     {
;     ^
;         fixed (char* charsPtr = &MemoryMarshal.GetReference(chars))
;                ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         fixed (byte* bytesPtr = &bytes)
;                ^^^^^^^^^^^^^^^^^^^^^^^
;             return Utf8NoBom.GetBytes(charsPtr, chars.Length, bytesPtr, byteCount);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       sub       rsp,38
       mov       r9,[rcx]
       mov       [rsp+30],r9
       mov       rax,r9
       mov       [rsp+28],rdx
       mov       r9d,[rcx+8]
       mov       [rsp+20],r8d
       mov       r8d,r9d
       mov       r9,rdx
       mov       rcx,16B7F8012F8
       mov       rcx,[rcx]
       mov       rdx,rax
       call      qword ptr [7FFC5443CCA8]; System.Text.UTF8Encoding.GetBytes(Char*, Int32, Byte*, Int32)
       nop
       add       rsp,38
       ret
; Total bytes of code 63
```

## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: DefaultJob)

```asm
; Benchmark.WriteString.Unicode()
;         var writer = new BshoxWriter(fixedBufferWriter);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         for (int i = 0; i < Count; i++)
;              ^^^^^^^^^
;             writer.WriteString(_stringsUnicode[i]);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         fixedBufferWriter.Reset();
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^
;         return writer.UnflushedBytes;
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       push      rbp
       push      r15
       push      r14
       push      r13
       push      r12
       push      rdi
       push      rsi
       push      rbx
       sub       rsp,178
       lea       rbp,[rsp+1B0]
       xor       eax,eax
       mov       [rbp-148],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rbp-140],xmm4
       mov       rax,0FFFFFFFFFFFFFF10
M00_L00:
       vmovdqa   xmmword ptr [rbp+rax-40],xmm4
       vmovdqa   xmmword ptr [rbp+rax-30],xmm4
       vmovdqa   xmmword ptr [rbp+rax-20],xmm4
       add       rax,30
       jne       short M00_L00
       mov       rsi,rcx
       mov       rdi,[rsi+18]
       xor       ebx,ebx
       xor       r14d,r14d
       xor       r15d,r15d
       mov       r8,2B122C012F8
       mov       r13,[r8]
       xor       r12d,r12d
       jmp       near ptr M00_L07
M00_L01:
       mov       [rbp-8C],r8d
       mov       ecx,edx
M00_L02:
       lea       edx,[rcx+1]
       mov       r8d,[rbp-8C]
       mov       r11d,r8d
       or        r11d,0FFFFFF80
       movsxd    rcx,ecx
       mov       [rbx+rcx],r11b
       shr       r8d,7
       cmp       r8d,7F
       ja        short M00_L01
M00_L03:
       movsxd    rcx,edx
       mov       [rbx+rcx],r8b
       lea       ecx,[rdx+1]
       mov       [rbp-0C4],ecx
       test      ecx,ecx
       jl        near ptr M00_L45
       mov       edx,ecx
       add       rbx,rdx
       sub       r14d,ecx
       add       r15d,ecx
       cmp       r14d,eax
       jl        near ptr M00_L11
M00_L04:
       mov       r10,[rbp-158]
       add       r10,0C
       mov       r11d,[rbp-14C]
       mov       [rbp-11C],r11d
       xor       ecx,ecx
       mov       [rbp-100],rcx
       mov       [rbp-108],rcx
       mov       [rbp-100],r10
       mov       [rbp-130],r10
       mov       [rbp-108],rbx
       mov       r9,rbx
       mov       [rbp-138],r9
       test      r10,r10
       je        near ptr M00_L46
       test      r9,r9
       je        near ptr M00_L47
       mov       eax,[rbp-3C]
       mov       ecx,r11d
       or        ecx,eax
       jl        near ptr M00_L49
       mov       r8,[rbp-170]
       mov       [rbp-168],r8
       lea       rcx,[rbp-110]
       mov       [rsp+20],rcx
       lea       rcx,[rbp-118]
       mov       [rsp+28],rcx
       mov       rcx,r10
       mov       edx,r11d
       mov       r8,r9
       mov       r9d,eax
       call      qword ptr [7FFC54337C00]; System.Text.Unicode.Utf8Utility.TranscodeToUtf8(Char*, Int32, Byte*, Int32, Char* ByRef, Byte* ByRef)
       mov       rcx,[rbp-110]
       mov       rdx,[rbp-130]
       sub       rcx,rdx
       mov       r8,rcx
       shr       r8,3F
       add       rcx,r8
       sar       rcx,1
       mov       r8d,[rbp-118]
       mov       r9,[rbp-138]
       sub       r8d,r9d
       mov       r11d,[rbp-11C]
       cmp       ecx,r11d
       jne       near ptr M00_L42
M00_L05:
       xor       ecx,ecx
       mov       [rbp-100],rcx
       mov       [rbp-108],rcx
       mov       ecx,[rbp-3C]
       test      ecx,ecx
       jl        near ptr M00_L50
       mov       edx,ecx
       add       rbx,rdx
       sub       r14d,ecx
       add       r15d,ecx
M00_L06:
       inc       r12d
       cmp       r12d,3E8
       jge       near ptr M00_L10
M00_L07:
       mov       r8,[rsi+8]
       cmp       r12d,[r8+8]
       jae       near ptr M00_L51
       mov       rax,[r8+r12*8+10]
       mov       [rbp-158],rax
       mov       r10d,[rax+8]
       mov       [rbp-14C],r10d
       test      r10d,r10d
       je        near ptr M00_L27
       cmp       r10d,2A
       jle       near ptr M00_L32
       lea       r8,[rax+0C]
       mov       [rbp-70],r8
       mov       r11,[rbp-70]
       mov       [rbp-128],r11
       mov       r8,2B122C01300
       mov       rdx,[r8]
       mov       [rbp-170],rdx
       mov       [rbp-160],rdx
       mov       [rbp-74],r10d
       lea       r8,[rbp-80]
       lea       r9,[rbp-88]
       mov       rcx,r11
       mov       edx,[rbp-74]
       call      qword ptr [7FFC54337AE0]; System.Text.Unicode.Utf16Utility.GetPointerToFirstInvalidChar(Char*, Int32, Int64 ByRef, Int32 ByRef)
       mov       rdx,[rbp-128]
       sub       rax,rdx
       mov       r9,rax
       shr       r9,3F
       add       r9,rax
       sar       r9,1
       movsxd    rax,r9d
       add       rax,[rbp-80]
       cmp       rax,7FFFFFFF
       ja        near ptr M00_L44
       mov       [rbp-78],eax
       mov       r8d,[rbp-74]
       cmp       r9d,r8d
       jne       near ptr M00_L37
M00_L08:
       xor       ecx,ecx
       mov       [rbp-70],rcx
       mov       eax,[rbp-78]
       mov       [rbp-3C],eax
       mov       [rbp-8C],eax
       cmp       r14d,5
       jl        near ptr M00_L19
M00_L09:
       xor       ecx,ecx
       mov       eax,[rbp-3C]
       cmp       eax,7F
       ja        near ptr M00_L02
       mov       edx,ecx
       mov       r8d,[rbp-8C]
       jmp       near ptr M00_L03
M00_L10:
       mov       rdi,[rsi+18]
       lea       rsi,[rdi+18]
       add       rdi,8
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,r15d
       add       rsp,178
       pop       rbx
       pop       rsi
       pop       rdi
       pop       r12
       pop       r13
       pop       r14
       pop       r15
       pop       rbp
       ret
M00_L11:
       test      r15d,r15d
       jg        near ptr M00_L40
M00_L12:
       mov       r8d,[r13+0C]
       mov       eax,[rbp-3C]
       cmp       eax,r8d
       jl        short M00_L13
       mov       r8d,eax
       mov       eax,[rbp-3C]
M00_L13:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rdi],rdx
       jne       near ptr M00_L41
       lea       rdx,[rbp-0E8]
       mov       rcx,rdi
       call      qword ptr [7FFC54596160]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       ebx,ebx
       xor       r14d,r14d
       mov       rcx,[rbp-0E8]
       test      rcx,rcx
       je        short M00_L16
       mov       rax,[rcx]
       mov       rdx,rax
       test      dword ptr [rdx],80000000
       jne       short M00_L14
       lea       rdx,[rbp-0F8]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       r14d,[rbp-0F0]
       mov       rbx,[rbp-0F8]
       jmp       short M00_L15
M00_L14:
       lea       rbx,[rcx+10]
       mov       r14d,[rcx+8]
M00_L15:
       mov       edx,[rbp-0E0]
       and       edx,7FFFFFFF
       mov       ecx,[rbp-0DC]
       mov       r8d,ecx
       add       r8,rdx
       mov       r11d,r14d
       cmp       r8,r11
       ja        short M00_L18
       add       rbx,rdx
       mov       r14d,ecx
M00_L16:
       mov       rdx,rbx
       mov       [rbp-0D8],rdx
       mov       [rbp-0D0],r14d
M00_L17:
       cmp       dword ptr [rbp-0D0],0
       jbe       near ptr M00_L51
       mov       rbx,[rbp-0D8]
       mov       r14d,[rbp-0D0]
       jmp       near ptr M00_L04
M00_L18:
       call      qword ptr [7FFC54507990]
       int       3
M00_L19:
       test      r15d,r15d
       jg        near ptr M00_L38
M00_L20:
       mov       edx,[r13+0C]
       cmp       edx,5
       jg        short M00_L21
       mov       r10d,5
       jmp       short M00_L22
M00_L21:
       mov       r10d,edx
M00_L22:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rdi],rdx
       jne       near ptr M00_L39
       lea       rdx,[rbp-0B0]
       mov       rcx,rdi
       mov       r8d,r10d
       call      qword ptr [7FFC54596160]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       ebx,ebx
       xor       r14d,r14d
       mov       rcx,[rbp-0B0]
       test      rcx,rcx
       je        short M00_L25
       mov       rax,[rcx]
       mov       rdx,rax
       test      dword ptr [rdx],80000000
       jne       short M00_L23
       lea       rdx,[rbp-0C0]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       r14d,[rbp-0B8]
       mov       rbx,[rbp-0C0]
       jmp       short M00_L24
M00_L23:
       lea       rbx,[rcx+10]
       mov       r14d,[rcx+8]
M00_L24:
       mov       edx,[rbp-0A8]
       and       edx,7FFFFFFF
       mov       ecx,[rbp-0A4]
       mov       r8d,ecx
       add       r8,rdx
       mov       r11d,r14d
       cmp       r8,r11
       ja        near ptr M00_L18
       add       rbx,rdx
       mov       r14d,ecx
M00_L25:
       mov       [rbp-0A0],rbx
       mov       [rbp-98],r14d
M00_L26:
       cmp       dword ptr [rbp-98],0
       jbe       near ptr M00_L51
       mov       rbx,[rbp-0A0]
       mov       r14d,[rbp-98]
       jmp       near ptr M00_L09
M00_L27:
       test      r14d,r14d
       jg        short M00_L31
       test      r15d,r15d
       jle       short M00_L28
       mov       rcx,rdi
       mov       edx,r15d
       mov       r11,7FFC54280588
       call      qword ptr [r11]
       xor       r15d,r15d
M00_L28:
       mov       r8d,[r13+0C]
       cmp       r8d,1
       jle       short M00_L29
       jmp       short M00_L30
M00_L29:
       mov       r8d,1
M00_L30:
       lea       rdx,[rbp-50]
       mov       rcx,rdi
       mov       r11,7FFC54280580
       call      qword ptr [r11]
       cmp       dword ptr [rbp-48],0
       jbe       near ptr M00_L51
       mov       rbx,[rbp-50]
       mov       r14d,[rbp-48]
M00_L31:
       mov       byte ptr [rbx],0
       inc       rbx
       dec       r14d
       inc       r15d
       jmp       near ptr M00_L06
M00_L32:
       cmp       r14d,80
       jge       short M00_L36
       test      r15d,r15d
       jle       short M00_L33
       mov       rcx,rdi
       mov       edx,r15d
       mov       r11,7FFC54280598
       call      qword ptr [r11]
       xor       r15d,r15d
       mov       rax,[rbp-158]
       mov       r10d,[rbp-14C]
M00_L33:
       mov       r8d,[r13+0C]
       cmp       r8d,80
       jle       short M00_L34
       jmp       short M00_L35
M00_L34:
       mov       r8d,80
M00_L35:
       lea       rdx,[rbp-60]
       mov       rcx,rdi
       mov       r11,7FFC54280590
       call      qword ptr [r11]
       cmp       dword ptr [rbp-58],0
       jbe       near ptr M00_L51
       mov       rbx,[rbp-60]
       mov       r14d,[rbp-58]
       mov       rax,[rbp-158]
       mov       r10d,[rbp-14C]
M00_L36:
       lea       rcx,[rax+0C]
       mov       edx,r10d
       mov       [rbp-148],rcx
       mov       [rbp-140],edx
       lea       rcx,[rbp-148]
       lea       rdx,[rbx+1]
       mov       r8d,7F
       call      qword ptr [7FFC5469FD20]; Bshox.Internals.EncodingHelper.Utf8Encode(System.ReadOnlySpan`1<Char>, Byte ByRef, Int32)
       mov       [rbx],al
       lea       ecx,[rax+1]
       mov       [rbp-64],ecx
       test      ecx,ecx
       jl        near ptr M00_L43
       mov       edx,ecx
       add       rbx,rdx
       sub       r14d,ecx
       add       r15d,ecx
       jmp       near ptr M00_L06
M00_L37:
       mov       rcx,[rbp-160]
       call      qword ptr [7FFC54714150]
       add       eax,[rbp-78]
       mov       ecx,eax
       test      ecx,ecx
       mov       [rbp-78],ecx
       jge       near ptr M00_L08
       jmp       near ptr M00_L44
M00_L38:
       mov       rcx,rdi
       mov       edx,r15d
       mov       r11,7FFC542805A8
       call      qword ptr [r11]
       xor       r15d,r15d
       jmp       near ptr M00_L20
M00_L39:
       lea       rdx,[rbp-0A0]
       mov       rcx,rdi
       mov       r8d,r10d
       mov       r11,7FFC542805A0
       call      qword ptr [r11]
       jmp       near ptr M00_L26
M00_L40:
       mov       rcx,rdi
       mov       edx,r15d
       mov       r11,7FFC542805B8
       call      qword ptr [r11]
       xor       r15d,r15d
       jmp       near ptr M00_L12
M00_L41:
       lea       rdx,[rbp-0D8]
       mov       rcx,rdi
       mov       r11,7FFC542805B0
       call      qword ptr [r11]
       jmp       near ptr M00_L17
M00_L42:
       mov       eax,[rbp-3C]
       mov       [rsp+20],eax
       mov       [rsp+28],ecx
       mov       [rsp+30],r8d
       mov       dword ptr [rsp+38],1
       mov       rcx,[rbp-168]
       mov       r8d,r11d
       call      qword ptr [7FFC54714138]
       jmp       near ptr M00_L05
M00_L43:
       mov       ecx,225
       mov       rdx,7FFC546F3D90
       call      qword ptr [7FFC5433EE38]
       mov       rdx,rax
       mov       ecx,[rbp-64]
       call      qword ptr [7FFC54714108]
       int       3
M00_L44:
       call      qword ptr [7FFC54714180]
       int       3
M00_L45:
       mov       ecx,225
       mov       rdx,7FFC546F3D90
       call      qword ptr [7FFC5433EE38]
       mov       rdx,rax
       mov       ecx,[rbp-0C4]
       call      qword ptr [7FFC54714108]
       int       3
M00_L46:
       mov       ecx,0E
       jmp       short M00_L48
M00_L47:
       mov       ecx,0A
M00_L48:
       mov       edx,31
       call      qword ptr [7FFC54714168]
       int       3
M00_L49:
       mov       ecx,0C
       mov       edx,0D
       call      qword ptr [7FFC54695770]
       int       3
M00_L50:
       mov       ecx,225
       mov       rdx,7FFC546F3D90
       call      qword ptr [7FFC5433EE38]
       mov       rdx,rax
       mov       ecx,[rbp-3C]
       call      qword ptr [7FFC54714108]
       int       3
M00_L51:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 1870
```
```asm
; System.Text.Unicode.Utf8Utility.TranscodeToUtf8(Char*, Int32, Byte*, Int32, Char* ByRef, Byte* ByRef)
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       mov       r10,[rsp+48]
       mov       r11,[rsp+50]
       cmp       edx,r9d
       mov       eax,r9d
       cmovle    eax,edx
       xor       ebx,ebx
       cmp       rax,20
       jb        near ptr M01_L37
       mov       rsi,[rcx]
       mov       rdi,0FF80FF80FF80FF80
       test      rdi,rsi
       je        near ptr M01_L24
M01_L00:
       mov       eax,esi
       test      eax,0FF80FF80
       je        near ptr M01_L43
M01_L01:
       test      eax,0FF80
       je        near ptr M01_L44
M01_L02:
       lea       rcx,[rcx+rbx*2]
       add       r8,rbx
       cmp       ebx,edx
       je        near ptr M01_L45
       sub       edx,ebx
       sub       r9d,ebx
       cmp       edx,2
       jl        short M01_L04
       mov       eax,edx
       lea       rax,[rcx+rax*2-4]
       jmp       near ptr M01_L22
M01_L03:
       sub       rax,rcx
       mov       rdx,rax
       shr       rdx,3F
       add       rdx,rax
       sar       rdx,1
       add       edx,2
M01_L04:
       test      edx,edx
       je        short M01_L07
       movzx     eax,word ptr [rcx]
M01_L05:
       cmp       eax,7F
       jbe       near ptr M01_L59
       cmp       eax,800
       jb        near ptr M01_L23
       lea       ebx,[rax-0D800]
       cmp       ebx,7FF
       jbe       near ptr M01_L60
       cmp       r9d,3
       jl        near ptr M01_L61
       mov       r9d,eax
       and       r9d,3F
       or        r9d,0FFFFFF80
       mov       [r8+2],r9b
       mov       r9d,eax
       shr       r9d,6
       and       r9d,3F
       or        r9d,0FFFFFF80
       mov       [r8+1],r9b
       shr       eax,0C
       or        eax,0FFFFFFE0
       mov       [r8],al
       add       rcx,2
       add       r8,3
M01_L06:
       cmp       edx,1
       jg        near ptr M01_L61
M01_L07:
       xor       eax,eax
M01_L08:
       mov       [r10],rcx
       mov       [r11],r8
       vzeroupper
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M01_L09:
       cmp       r9d,2
       jl        near ptr M01_L58
       mov       esi,ebx
       shr       esi,8
       or        esi,ebx
       mov       [r8],si
       add       rcx,4
       add       r8,2
       add       r9d,0FFFFFFFE
       mov       rbx,rax
       sub       rbx,rcx
       mov       rsi,rbx
       shr       rsi,3F
       add       rbx,rsi
       sar       rbx,1
       add       ebx,2
       movsxd    rsi,r9d
       cmp       rbx,rsi
       jle       short M01_L13
M01_L10:
       mov       ebx,esi
       shr       ebx,3
       xor       edi,edi
       test      ebx,ebx
       je        near ptr M01_L47
M01_L11:
       vmovups   xmm0,[rcx]
       vptest    xmm0,xmmword ptr [7FFC543AE740]
       je        near ptr M01_L46
       shl       edi,3
       sub       r9d,edi
       vmovq     rbx,xmm0
       mov       rsi,0FF80FF80FF80FF80
       test      rsi,rbx
       je        near ptr M01_L48
M01_L12:
       mov       esi,ebx
       test      esi,0FF80FF80
       je        near ptr M01_L49
       mov       ebx,esi
       jmp       near ptr M01_L17
M01_L13:
       mov       rsi,rbx
       jmp       short M01_L10
M01_L14:
       cmp       r9d,3
       jl        near ptr M01_L61
       lea       esi,[rbx*4]
       and       esi,3F00
       movzx     edi,bx
       shr       edi,0C
       add       esi,edi
       add       esi,80E0
       mov       [r8],si
       mov       esi,ebx
       and       esi,3F
       or        esi,0FFFFFF80
       mov       [r8+2],sil
       add       rcx,2
       add       r8,3
       add       r9d,0FFFFFFFD
       cmp       ebx,800000
       jae       near ptr M01_L51
       test      r9d,r9d
       je        near ptr M01_L61
       shr       ebx,10
       mov       [r8],bl
       add       rcx,2
       inc       r8
       dec       r9d
       cmp       rcx,rax
       ja        near ptr M01_L03
       mov       ebx,[rcx]
       test      ebx,0F800
       je        near ptr M01_L16
M01_L15:
       lea       esi,[rbx-0D800]
       test      esi,0F800
       je        near ptr M01_L53
       test      ebx,0F8000000
       je        near ptr M01_L14
       lea       esi,[rbx+28000000]
       cmp       esi,8000000
       jb        near ptr M01_L14
       cmp       r9d,6
       jl        near ptr M01_L14
       lea       esi,[rbx*4]
       and       esi,3F00
       mov       edi,ebx
       and       edi,3F
       shl       edi,10
       or        esi,edi
       mov       edi,ebx
       shr       edi,4
       and       edi,0F000000
       mov       ebp,ebx
       shr       ebp,0C
       and       ebp,0F
       or        edi,ebp
       add       esi,edi
       add       esi,0E08080E0
       mov       [r8],esi
       mov       esi,ebx
       shr       esi,16
       and       esi,3F
       shr       ebx,8
       and       ebx,3F00
       add       ebx,esi
       add       ebx,8080
       mov       [r8+4],bx
       add       rcx,4
       add       r8,6
       add       r9d,0FFFFFFFA
       cmp       rcx,rax
       ja        near ptr M01_L03
       mov       ebx,[rcx]
       test      ebx,0F800
       jne       near ptr M01_L15
M01_L16:
       test      ebx,0FF80FF80
       je        near ptr M01_L09
M01_L17:
       test      ebx,0FF80
       jne       short M01_L18
       test      r9d,r9d
       je        near ptr M01_L61
       mov       [r8],bl
       add       rcx,2
       inc       r8
       dec       r9d
       cmp       rcx,rax
       ja        near ptr M01_L03
       mov       ebx,[rcx]
M01_L18:
       test      ebx,0F800
       jne       near ptr M01_L15
       lea       esi,[rbx-800000]
       cmp       esi,77FFFFF
       ja        short M01_L20
M01_L19:
       cmp       r9d,4
       jl        near ptr M01_L58
       mov       esi,ebx
       shr       esi,6
       and       esi,1F001F
       shl       ebx,8
       and       ebx,3F003F00
       add       ebx,esi
       add       ebx,80C080C0
       mov       [r8],ebx
       add       rcx,4
       add       r8,4
       add       r9d,0FFFFFFFC
       cmp       rcx,rax
       ja        near ptr M01_L03
       mov       ebx,[rcx]
       lea       esi,[rbx-80]
       movzx     esi,si
       cmp       esi,780
       jge       near ptr M01_L16
       lea       esi,[rbx-800000]
       cmp       esi,77FFFFF
       jbe       short M01_L19
M01_L20:
       cmp       r9d,2
       jl        near ptr M01_L61
       lea       esi,[rbx*4]
       and       esi,1F00
       mov       edi,ebx
       and       edi,3F
       lea       esi,[rsi+rdi+0C080]
       movbe     [r8],si
       cmp       ebx,800000
       jae       near ptr M01_L50
       cmp       r9d,3
       jl        near ptr M01_L57
       shr       ebx,10
       mov       [r8+2],bl
       add       rcx,4
       add       r8,3
       add       r9d,0FFFFFFFD
M01_L21:
       cmp       rcx,rax
       ja        near ptr M01_L03
M01_L22:
       mov       ebx,[rcx]
       jmp       near ptr M01_L16
M01_L23:
       cmp       r9d,2
       jl        near ptr M01_L61
       mov       r9d,eax
       and       r9d,3F
       or        r9d,0FFFFFF80
       mov       [r8+1],r9b
       shr       eax,6
       or        eax,0FFFFFFC0
       mov       [r8],al
       add       rcx,2
       add       r8,2
       jmp       near ptr M01_L06
M01_L24:
       cmp       rax,40
       jb        near ptr M01_L31
       mov       rbx,rcx
       vmovups   ymm0,[rbx]
       vptest    ymm0,ymmword ptr [7FFC543AE760]
       je        short M01_L25
       xor       ebx,ebx
       jmp       near ptr M01_L30
M01_L25:
       mov       rsi,r8
       vpackuswb ymm0,ymm0,ymm0
       vpermq    ymm0,ymm0,0D8
       vmovups   [rsi],xmm0
       mov       edi,10
       test      r8b,10
       jne       short M01_L26
       vmovups   ymm0,[rbx+20]
       vptest    ymm0,ymmword ptr [7FFC543AE760]
       jne       short M01_L28
       vpackuswb ymm0,ymm0,ymm0
       vpermq    ymm0,ymm0,0D8
       vmovups   [rsi+10],xmm0
M01_L26:
       mov       rdi,r8
       and       rdi,1F
       neg       rdi
       add       rdi,20
       lea       rbp,[rax-20]
M01_L27:
       vmovups   ymm0,[rbx+rdi*2]
       vmovups   ymm1,[rbx+rdi*2+20]
       vpor      ymm2,ymm0,ymm1
       vptest    ymm2,ymmword ptr [7FFC543AE760]
       jne       short M01_L29
       vpackuswb ymm0,ymm0,ymm1
       vpermq    ymm0,ymm0,0D8
       vmovups   [rsi+rdi],ymm0
       add       rdi,20
       cmp       rdi,rbp
       jbe       short M01_L27
M01_L28:
       mov       rbx,rdi
       jmp       short M01_L30
M01_L29:
       vptest    ymm0,ymmword ptr [7FFC543AE760]
       jne       short M01_L28
       vpackuswb ymm0,ymm0,ymm0
       vpermq    ymm0,ymm0,0D8
       vmovups   [rsi+rdi],xmm0
       add       rdi,10
       jmp       short M01_L28
M01_L30:
       jmp       near ptr M01_L37
M01_L31:
       mov       rbx,rcx
       vmovups   xmm0,[rbx]
       vptest    xmm0,xmmword ptr [7FFC543AE740]
       je        short M01_L32
       xor       ebx,ebx
       jmp       near ptr M01_L37
M01_L32:
       mov       rsi,r8
       vpackuswb xmm0,xmm0,xmm0
       vmovsd    qword ptr [rsi],xmm0
       mov       edi,8
       test      r8b,8
       jne       short M01_L33
       vmovups   xmm0,[rbx+10]
       vptest    xmm0,xmmword ptr [7FFC543AE740]
       jne       short M01_L35
       vpackuswb xmm0,xmm0,xmm0
       vmovsd    qword ptr [rsi+8],xmm0
M01_L33:
       mov       rdi,r8
       and       rdi,0F
       neg       rdi
       add       rdi,10
       lea       rbp,[rax-10]
M01_L34:
       vmovups   xmm0,[rbx+rdi*2]
       vmovups   xmm1,[rbx+rdi*2+10]
       vpor      xmm2,xmm0,xmm1
       vptest    xmm2,xmmword ptr [7FFC543AE740]
       jne       short M01_L36
       vpackuswb xmm0,xmm0,xmm1
       vmovups   [rsi+rdi],xmm0
       add       rdi,10
       cmp       rdi,rbp
       jbe       short M01_L34
M01_L35:
       mov       rbx,rdi
       jmp       short M01_L37
M01_L36:
       vptest    xmm0,xmmword ptr [7FFC543AE740]
       jne       short M01_L35
       vpackuswb xmm0,xmm0,xmm0
       vmovsd    qword ptr [rsi+rdi],xmm0
       add       rdi,8
       jmp       short M01_L35
M01_L37:
       sub       rax,rbx
       cmp       rax,4
       jb        short M01_L39
       lea       rsi,[rbx+rax-4]
M01_L38:
       mov       rdi,[rcx+rbx*2]
       mov       rbp,0FF80FF80FF80FF80
       test      rbp,rdi
       jne       short M01_L42
       vmovq     xmm0,rdi
       vpackuswb xmm0,xmm0,xmm0
       vmovd     dword ptr [r8+rbx],xmm0
       add       rbx,4
       cmp       rbx,rsi
       jbe       short M01_L38
M01_L39:
       test      al,2
       je        short M01_L40
       mov       esi,[rcx+rbx*2]
       test      esi,0FF80FF80
       jne       short M01_L41
       lea       rdi,[r8+rbx]
       mov       [rdi],sil
       shr       esi,10
       mov       [rdi+1],sil
       add       rbx,2
M01_L40:
       test      al,1
       je        near ptr M01_L02
       movzx     eax,word ptr [rcx+rbx*2]
       cmp       eax,7F
       ja        near ptr M01_L02
       jmp       short M01_L44
M01_L41:
       mov       eax,esi
       jmp       near ptr M01_L01
M01_L42:
       mov       rsi,rdi
       jmp       near ptr M01_L00
M01_L43:
       lea       rdi,[r8+rbx]
       mov       [rdi],al
       shr       eax,10
       mov       [rdi+1],al
       shr       rsi,20
       mov       eax,esi
       add       rbx,2
       jmp       near ptr M01_L01
M01_L44:
       mov       [r8+rbx],al
       inc       rbx
       jmp       near ptr M01_L02
M01_L45:
       mov       [r10],rcx
       mov       [r11],r8
       xor       eax,eax
       vzeroupper
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M01_L46:
       vpackuswb xmm0,xmm0,xmm0
       vmovq     qword ptr [r8],xmm0
       add       rcx,10
       add       r8,8
       inc       edi
       cmp       edi,ebx
       jb        near ptr M01_L11
M01_L47:
       lea       ebx,[rdi*8]
       sub       r9d,ebx
       test      sil,4
       je        near ptr M01_L21
       mov       rbx,[rcx]
       mov       rsi,0FF80FF80FF80FF80
       test      rsi,rbx
       jne       near ptr M01_L12
       jmp       short M01_L52
M01_L48:
       vpackuswb xmm1,xmm0,xmm0
       vmovd     dword ptr [r8],xmm1
       add       rcx,8
       add       r8,4
       add       r9d,0FFFFFFFC
       vpextrq   rbx,xmm0,1
       jmp       near ptr M01_L12
M01_L49:
       mov       edi,esi
       shr       edi,8
       or        edi,esi
       mov       [r8],di
       add       rcx,4
       add       r8,2
       add       r9d,0FFFFFFFE
       shr       rbx,20
       mov       esi,ebx
       mov       ebx,esi
       jmp       near ptr M01_L17
M01_L50:
       add       rcx,2
       add       r8,2
       add       r9d,0FFFFFFFE
       cmp       rcx,rax
       ja        near ptr M01_L03
       mov       ebx,[rcx]
       jmp       near ptr M01_L15
M01_L51:
       cmp       rcx,rax
       ja        near ptr M01_L03
       mov       ebx,[rcx]
       jmp       near ptr M01_L17
M01_L52:
       vmovq     xmm0,rbx
       vpackuswb xmm0,xmm0,xmm0
       vmovd     dword ptr [r8],xmm0
       add       rcx,8
       jmp       short M01_L56
M01_L53:
       lea       esi,[rbx+23FF2800]
       test      esi,0FC00FC00
       jne       short M01_L54
       cmp       r9d,4
       jl        near ptr M01_L61
       jmp       short M01_L55
M01_L54:
       mov       eax,3
       jmp       near ptr M01_L08
M01_L55:
       add       ebx,40
       mov       esi,ebx
       and       esi,3
       shl       esi,14
       or        esi,808080F0
       mov       edi,ebx
       and       edi,3F0700
       bswap     edi
       rol       edi,10
       or        esi,edi
       mov       edi,ebx
       shr       edi,6
       and       edi,0F0000
       or        esi,edi
       and       ebx,0FC
       shl       ebx,6
       or        ebx,esi
       mov       [r8],ebx
       add       rcx,4
M01_L56:
       add       r8,4
       add       r9d,0FFFFFFFC
       jmp       near ptr M01_L21
M01_L57:
       add       rcx,2
       add       r8,2
       jmp       short M01_L61
M01_L58:
       movzx     eax,bx
       jmp       near ptr M01_L05
M01_L59:
       test      r9d,r9d
       je        short M01_L61
       mov       [r8],al
       add       rcx,2
       inc       r8
       jmp       near ptr M01_L06
M01_L60:
       cmp       eax,0DBFF
       ja        near ptr M01_L54
       mov       eax,2
       jmp       near ptr M01_L08
M01_L61:
       mov       eax,1
       jmp       near ptr M01_L08
; Total bytes of code 1980
```
```asm
; System.Text.Unicode.Utf16Utility.GetPointerToFirstInvalidChar(Char*, Int32, Int64 ByRef, Int32 ByRef)
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       mov       rax,rcx
       mov       r10d,edx
       mov       r11,rax
       cmp       r10,20
       jae       near ptr M02_L13
       cmp       r10,10
       jae       near ptr M02_L11
M02_L00:
       cmp       r10,4
       jb        short M02_L02
M02_L01:
       mov       r11d,[rax]
       mov       ebx,[rax+4]
       mov       esi,r11d
       or        esi,ebx
       test      esi,0FF80FF80
       jne       near ptr M02_L16
       add       rax,8
       add       r10,0FFFFFFFFFFFFFFFC
       cmp       r10,4
       jae       short M02_L01
M02_L02:
       test      r10b,2
       je        short M02_L03
       mov       r11d,[rax]
       test      r11d,0FF80FF80
       jne       near ptr M02_L17
       add       rax,4
M02_L03:
       test      r10b,1
       je        short M02_L05
       cmp       word ptr [rax],7F
       ja        short M02_L05
M02_L04:
       add       rax,2
M02_L05:
       sub       rax,rcx
       shr       rax,1
       mov       r10d,eax
       lea       rcx,[rcx+r10*2]
       sub       edx,eax
       je        near ptr M02_L21
       xor       eax,eax
       xor       r10d,r10d
       mov       r11d,edx
       lea       r11,[rcx+r11*2]
       cmp       edx,8
       jl        short M02_L08
       vbroadcastss xmm0,dword ptr [7FFC543AD440]
       vbroadcastss xmm1,dword ptr [7FFC543AD444]
       vbroadcastss xmm2,dword ptr [7FFC543AD448]
       lea       rdx,[r11-10]
M02_L06:
       vmovups   xmm3,[rcx]
       add       rcx,10
       vpminuw   xmm4,xmm3,xmm0
       vpaddusw  xmm5,xmm3,xmm1
       vpor      xmm4,xmm5,xmm4
       vpmovmskb ebx,xmm4
       popcnt    ebx,ebx
       vpaddw    xmm4,xmm3,xmm2
       vpcmpgtw  xmm4,xmm1,xmm4
       vpmovmskb esi,xmm4
M02_L07:
       cmp       esi,0FFFF
       jne       near ptr M02_L22
       add       rax,rbx
       cmp       rcx,rdx
       jbe       short M02_L06
M02_L08:
       cmp       rcx,r11
       jb        near ptr M02_L18
M02_L09:
       mov       [r8],rax
       mov       [r9],r10d
M02_L10:
       mov       rax,rcx
       vzeroupper
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M02_L11:
       vbroadcastss xmm0,dword ptr [7FFC543AD44C]
       vptest    xmm0,xmmword ptr [rax]
       jne       near ptr M02_L00
       lea       rbx,[r11+r10*2-10]
       add       r11,10
       mov       rax,r11
       and       rax,0FFFFFFFFFFFFFFF0
       vpand     xmm1,xmm0,[rax]
       vptest    xmm1,xmm1
       jne       short M02_L15
M02_L12:
       add       rax,10
       cmp       rax,rbx
       ja        short M02_L15
       vpand     xmm1,xmm0,[rax]
       vptest    xmm1,xmm1
       jne       short M02_L15
       jmp       short M02_L12
M02_L13:
       vbroadcastss ymm0,dword ptr [7FFC543AD44C]
       vptest    ymm0,ymmword ptr [rax]
       jne       near ptr M02_L00
       lea       rbx,[r11+r10*2-20]
       lea       rax,[r11+20]
       and       rax,0FFFFFFFFFFFFFFE0
       vpand     ymm1,ymm0,[rax]
       vptest    ymm1,ymm1
       jne       short M02_L15
M02_L14:
       add       rax,20
       cmp       rax,rbx
       ja        short M02_L15
       vpand     ymm1,ymm0,[rax]
       vptest    ymm1,ymm1
       je        short M02_L14
M02_L15:
       mov       r11,rax
       sub       r11,rcx
       shr       r11,1
       sub       r10,r11
       jmp       near ptr M02_L00
M02_L16:
       test      r11d,0FF80FF80
       je        short M02_L20
M02_L17:
       test      r11d,0FF80
       jne       near ptr M02_L05
       jmp       near ptr M02_L04
M02_L18:
       movzx     edx,word ptr [rcx]
       cmp       edx,7F
       jbe       short M02_L19
       lea       ebx,[rdx+1F800]
       shr       ebx,10
       add       rax,rbx
       add       edx,0FFFF2800
       cmp       edx,7FF
       jbe       short M02_L24
M02_L19:
       add       rcx,2
       cmp       rcx,r11
       jb        short M02_L18
       jmp       near ptr M02_L09
M02_L20:
       mov       r11d,ebx
       add       rax,4
       jmp       short M02_L17
M02_L21:
       xor       eax,eax
       mov       [r8],rax
       mov       [r9],eax
       jmp       near ptr M02_L10
M02_L22:
       not       esi
       vpsrlw    xmm4,xmm3,3
       vpmovmskb edi,xmm4
       mov       ebp,edi
       and       ebp,esi
       xor       edi,5555
       and       esi,edi
       shl       esi,2
       movzx     edi,si
       cmp       edi,ebp
       jne       short M02_L25
       cmp       esi,0FFFF
       jbe       short M02_L23
       movzx     esi,si
       add       rbx,0FFFFFFFFFFFFFFFE
       add       rcx,0FFFFFFFFFFFFFFFE
M02_L23:
       popcnt    esi,esi
       sub       r10d,esi
       sub       rax,rsi
       sub       rax,rsi
       mov       esi,0FFFF
       jmp       near ptr M02_L07
M02_L24:
       add       rax,0FFFFFFFFFFFFFFFE
       mov       rdx,r11
       sub       rdx,rcx
       cmp       rdx,4
       jb        near ptr M02_L09
       mov       edx,[rcx]
       add       edx,23FF2800
       test      edx,0FC00FC00
       jne       near ptr M02_L09
       dec       r10d
       add       rax,2
       add       rcx,2
       jmp       near ptr M02_L19
M02_L25:
       add       rcx,0FFFFFFFFFFFFFFF0
       cmp       rcx,r11
       jae       near ptr M02_L09
       jmp       near ptr M02_L18
; Total bytes of code 670
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
       jg        short M03_L00
       vmovdqu   xmm0,xmmword ptr [rcx+8]
       vmovdqu   xmmword ptr [rdx],xmm0
       mov       rax,rdx
       add       rsp,28
       ret
M03_L00:
       mov       edx,[rcx+14]
       mov       ecx,r8d
       call      qword ptr [7FFC5469FE58]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 48
```
```asm
; Bshox.Internals.EncodingHelper.Utf8Encode(System.ReadOnlySpan`1<Char>, Byte ByRef, Int32)
;     {
;     ^
;         fixed (char* charsPtr = &MemoryMarshal.GetReference(chars))
;                ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         fixed (byte* bytesPtr = &bytes)
;                ^^^^^^^^^^^^^^^^^^^^^^^
;             return Utf8NoBom.GetBytes(charsPtr, chars.Length, bytesPtr, byteCount);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       sub       rsp,38
       mov       r9,[rcx]
       mov       [rsp+30],r9
       mov       rax,r9
       mov       [rsp+28],rdx
       mov       r9d,[rcx+8]
       mov       [rsp+20],r8d
       mov       r8d,r9d
       mov       r9,rdx
       mov       rcx,2B122C01300
       mov       rcx,[rcx]
       mov       rdx,rax
       call      qword ptr [7FFC5443CCA8]; System.Text.UTF8Encoding.GetBytes(Char*, Int32, Byte*, Int32)
       nop
       add       rsp,38
       ret
; Total bytes of code 63
```

## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: DefaultJob)

```asm
; Benchmark.WriteString.Ascii()
;         var writer = new BshoxWriter(fixedBufferWriter);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         for (int i = 0; i < Count; i++)
;              ^^^^^^^^^
;             writer.WriteString(_stringsAscii[i]);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         fixedBufferWriter.Reset();
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^
;         return writer.UnflushedBytes;
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       push      rbp
       push      r15
       push      r14
       push      r13
       push      r12
       push      rdi
       push      rsi
       push      rbx
       sub       rsp,178
       lea       rbp,[rsp+1B0]
       xor       eax,eax
       mov       [rbp-148],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rbp-140],xmm4
       mov       rax,0FFFFFFFFFFFFFF10
M00_L00:
       vmovdqa   xmmword ptr [rbp+rax-40],xmm4
       vmovdqa   xmmword ptr [rbp+rax-30],xmm4
       vmovdqa   xmmword ptr [rbp+rax-20],xmm4
       add       rax,30
       jne       short M00_L00
       mov       rsi,rcx
       mov       rdi,[rsi+18]
       xor       ebx,ebx
       xor       r14d,r14d
       xor       r15d,r15d
       mov       r8,20CCE0012F8
       mov       r13,[r8]
       xor       r12d,r12d
       jmp       near ptr M00_L12
M00_L01:
       mov       rdi,[rsi+18]
       lea       rsi,[rdi+18]
       add       rdi,8
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,r15d
       add       rsp,178
       pop       rbx
       pop       rsi
       pop       rdi
       pop       r12
       pop       r13
       pop       r14
       pop       r15
       pop       rbp
       ret
M00_L02:
       test      r15d,r15d
       jg        near ptr M00_L33
M00_L03:
       mov       edx,[r13+0C]
       cmp       edx,5
       jle       near ptr M00_L35
       jmp       near ptr M00_L34
M00_L04:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rdi],rdx
       jne       near ptr M00_L39
       lea       rdx,[rbp-0B0]
       mov       rcx,rdi
       mov       r8d,r10d
       call      qword ptr [7FFC545B6160]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       ebx,ebx
       xor       r14d,r14d
       mov       rcx,[rbp-0B0]
       test      rcx,rcx
       jne       near ptr M00_L36
M00_L05:
       mov       [rbp-0A0],rbx
       mov       [rbp-98],r14d
M00_L06:
       cmp       dword ptr [rbp-98],0
       jbe       near ptr M00_L55
       mov       rbx,[rbp-0A0]
       mov       r14d,[rbp-98]
       jmp       near ptr M00_L14
M00_L07:
       lea       edx,[rcx+1]
       mov       r8d,[rbp-8C]
       mov       r11d,r8d
       or        r11d,0FFFFFF80
       movsxd    rcx,ecx
       mov       [rbx+rcx],r11b
       shr       r8d,7
       cmp       r8d,7F
       mov       [rbp-8C],r8d
       mov       ecx,edx
       ja        short M00_L07
M00_L08:
       movsxd    rdx,ecx
       mov       r8d,[rbp-8C]
       mov       [rbx+rdx],r8b
       inc       ecx
       mov       [rbp-0C4],ecx
       test      ecx,ecx
       jl        near ptr M00_L50
       mov       edx,ecx
       add       rbx,rdx
       sub       r14d,ecx
       add       r15d,ecx
       cmp       r14d,eax
       jl        near ptr M00_L15
M00_L09:
       mov       r10,[rbp-158]
       add       r10,0C
       mov       r11d,[rbp-14C]
       mov       [rbp-11C],r11d
       xor       ecx,ecx
       mov       [rbp-100],rcx
       mov       [rbp-108],rcx
       mov       [rbp-100],r10
       mov       [rbp-130],r10
       mov       [rbp-108],rbx
       mov       r9,rbx
       mov       [rbp-138],r9
       test      r10,r10
       je        near ptr M00_L20
       test      r9,r9
       je        near ptr M00_L52
       mov       eax,[rbp-3C]
       mov       ecx,r11d
       or        ecx,eax
       jl        near ptr M00_L53
       mov       r8,[rbp-170]
       mov       [rbp-168],r8
       lea       rcx,[rbp-110]
       mov       [rsp+20],rcx
       lea       rcx,[rbp-118]
       mov       [rsp+28],rcx
       mov       rcx,r10
       mov       edx,r11d
       mov       r8,r9
       mov       r9d,eax
       call      qword ptr [7FFC54357C00]; System.Text.Unicode.Utf8Utility.TranscodeToUtf8(Char*, Int32, Byte*, Int32, Char* ByRef, Byte* ByRef)
       mov       rcx,[rbp-110]
       mov       rdx,[rbp-130]
       sub       rcx,rdx
       mov       r8,rcx
       shr       r8,3F
       add       rcx,r8
       sar       rcx,1
       mov       r8d,[rbp-118]
       mov       r9,[rbp-138]
       sub       r8d,r9d
       mov       r11d,[rbp-11C]
       cmp       ecx,r11d
       jne       near ptr M00_L47
M00_L10:
       xor       ecx,ecx
       mov       [rbp-100],rcx
       mov       [rbp-108],rcx
       mov       ecx,[rbp-3C]
       test      ecx,ecx
       jl        near ptr M00_L54
       mov       edx,ecx
       add       rbx,rdx
       sub       r14d,ecx
       add       r15d,ecx
M00_L11:
       inc       r12d
       cmp       r12d,3E8
       jge       near ptr M00_L01
M00_L12:
       mov       r8,[rsi+10]
       cmp       r12d,[r8+8]
       jae       near ptr M00_L55
       mov       rax,[r8+r12*8+10]
       mov       [rbp-158],rax
       mov       r10d,[rax+8]
       mov       [rbp-14C],r10d
       test      r10d,r10d
       je        near ptr M00_L22
       cmp       r10d,2A
       jle       near ptr M00_L27
       lea       r8,[rax+0C]
       mov       [rbp-70],r8
       mov       r11,[rbp-70]
       mov       [rbp-128],r11
       mov       r8,20CCE001300
       mov       rdx,[r8]
       mov       [rbp-170],rdx
       mov       [rbp-160],rdx
       mov       [rbp-74],r10d
       lea       r8,[rbp-80]
       lea       r9,[rbp-88]
       mov       rcx,r11
       mov       edx,[rbp-74]
       call      qword ptr [7FFC54357AE0]; System.Text.Unicode.Utf16Utility.GetPointerToFirstInvalidChar(Char*, Int32, Int64 ByRef, Int32 ByRef)
       mov       rdx,[rbp-128]
       sub       rax,rdx
       mov       r9,rax
       shr       r9,3F
       add       r9,rax
       sar       r9,1
       movsxd    rax,r9d
       add       rax,[rbp-80]
       cmp       rax,7FFFFFFF
       ja        near ptr M00_L49
       mov       [rbp-78],eax
       mov       r8d,[rbp-74]
       cmp       r9d,r8d
       jne       near ptr M00_L32
M00_L13:
       xor       ecx,ecx
       mov       [rbp-70],rcx
       mov       eax,[rbp-78]
       mov       [rbp-3C],eax
       mov       [rbp-8C],eax
       cmp       r14d,5
       jl        near ptr M00_L02
M00_L14:
       xor       ecx,ecx
       mov       eax,[rbp-3C]
       cmp       eax,7F
       jbe       near ptr M00_L08
       jmp       near ptr M00_L07
M00_L15:
       test      r15d,r15d
       jg        near ptr M00_L40
M00_L16:
       mov       r8d,[r13+0C]
       mov       eax,[rbp-3C]
       cmp       eax,r8d
       jge       near ptr M00_L42
       jmp       near ptr M00_L41
M00_L17:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rdi],rdx
       jne       near ptr M00_L46
       lea       rdx,[rbp-0E8]
       mov       rcx,rdi
       call      qword ptr [7FFC545B6160]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       ebx,ebx
       xor       r14d,r14d
       mov       rcx,[rbp-0E8]
       test      rcx,rcx
       jne       near ptr M00_L43
M00_L18:
       mov       rdx,rbx
       mov       [rbp-0D8],rdx
       mov       [rbp-0D0],r14d
M00_L19:
       cmp       dword ptr [rbp-0D0],0
       jbe       near ptr M00_L55
       mov       rbx,[rbp-0D8]
       mov       r14d,[rbp-0D0]
       jmp       near ptr M00_L09
M00_L20:
       mov       ecx,0E
M00_L21:
       mov       edx,31
       call      qword ptr [7FFC54734168]
       int       3
M00_L22:
       test      r14d,r14d
       jg        short M00_L26
       test      r15d,r15d
       jle       short M00_L23
       mov       rcx,rdi
       mov       edx,r15d
       mov       r11,7FFC542A0588
       call      qword ptr [r11]
       xor       r15d,r15d
M00_L23:
       mov       r8d,[r13+0C]
       cmp       r8d,1
       jle       short M00_L24
       jmp       short M00_L25
M00_L24:
       mov       r8d,1
M00_L25:
       lea       rdx,[rbp-50]
       mov       rcx,rdi
       mov       r11,7FFC542A0580
       call      qword ptr [r11]
       cmp       dword ptr [rbp-48],0
       jbe       near ptr M00_L55
       mov       rbx,[rbp-50]
       mov       r14d,[rbp-48]
M00_L26:
       mov       byte ptr [rbx],0
       inc       rbx
       dec       r14d
       inc       r15d
       jmp       near ptr M00_L11
M00_L27:
       cmp       r14d,80
       jge       short M00_L31
       test      r15d,r15d
       jle       short M00_L28
       mov       rcx,rdi
       mov       edx,r15d
       mov       r11,7FFC542A0598
       call      qword ptr [r11]
       xor       r15d,r15d
       mov       rax,[rbp-158]
       mov       r10d,[rbp-14C]
M00_L28:
       mov       r8d,[r13+0C]
       cmp       r8d,80
       jle       short M00_L29
       jmp       short M00_L30
M00_L29:
       mov       r8d,80
M00_L30:
       lea       rdx,[rbp-60]
       mov       rcx,rdi
       mov       r11,7FFC542A0590
       call      qword ptr [r11]
       cmp       dword ptr [rbp-58],0
       jbe       near ptr M00_L55
       mov       rbx,[rbp-60]
       mov       r14d,[rbp-58]
       mov       rax,[rbp-158]
       mov       r10d,[rbp-14C]
M00_L31:
       lea       rcx,[rax+0C]
       mov       edx,r10d
       mov       [rbp-148],rcx
       mov       [rbp-140],edx
       lea       rcx,[rbp-148]
       lea       rdx,[rbx+1]
       mov       r8d,7F
       call      qword ptr [7FFC546BFD20]; Bshox.Internals.EncodingHelper.Utf8Encode(System.ReadOnlySpan`1<Char>, Byte ByRef, Int32)
       mov       [rbx],al
       lea       ecx,[rax+1]
       mov       [rbp-64],ecx
       test      ecx,ecx
       jl        near ptr M00_L48
       mov       edx,ecx
       add       rbx,rdx
       sub       r14d,ecx
       add       r15d,ecx
       jmp       near ptr M00_L11
M00_L32:
       mov       rcx,[rbp-160]
       call      qword ptr [7FFC54734150]
       add       eax,[rbp-78]
       mov       ecx,eax
       test      ecx,ecx
       mov       [rbp-78],ecx
       jge       near ptr M00_L13
       jmp       near ptr M00_L49
M00_L33:
       mov       rcx,rdi
       mov       edx,r15d
       mov       r11,7FFC542A05A8
       call      qword ptr [r11]
       xor       r15d,r15d
       jmp       near ptr M00_L03
M00_L34:
       mov       r10d,edx
       jmp       near ptr M00_L04
M00_L35:
       mov       r10d,5
       jmp       near ptr M00_L04
M00_L36:
       mov       rax,[rcx]
       mov       rdx,rax
       test      dword ptr [rdx],80000000
       je        short M00_L37
       lea       rbx,[rcx+10]
       mov       r14d,[rcx+8]
       jmp       short M00_L38
M00_L37:
       lea       rdx,[rbp-0C0]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       r14d,[rbp-0B8]
       mov       rbx,[rbp-0C0]
M00_L38:
       mov       edx,[rbp-0A8]
       and       edx,7FFFFFFF
       mov       ecx,[rbp-0A4]
       mov       r8d,ecx
       add       r8,rdx
       mov       r11d,r14d
       cmp       r8,r11
       ja        near ptr M00_L51
       add       rbx,rdx
       mov       r14d,ecx
       jmp       near ptr M00_L05
M00_L39:
       lea       rdx,[rbp-0A0]
       mov       rcx,rdi
       mov       r8d,r10d
       mov       r11,7FFC542A05A0
       call      qword ptr [r11]
       jmp       near ptr M00_L06
M00_L40:
       mov       rcx,rdi
       mov       edx,r15d
       mov       r11,7FFC542A05B8
       call      qword ptr [r11]
       xor       r15d,r15d
       jmp       near ptr M00_L16
M00_L41:
       jmp       near ptr M00_L17
M00_L42:
       mov       r8d,eax
       mov       eax,[rbp-3C]
       jmp       near ptr M00_L17
M00_L43:
       mov       rax,[rcx]
       mov       rdx,rax
       test      dword ptr [rdx],80000000
       je        short M00_L44
       lea       rbx,[rcx+10]
       mov       r14d,[rcx+8]
       jmp       short M00_L45
M00_L44:
       lea       rdx,[rbp-0F8]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       r14d,[rbp-0F0]
       mov       rbx,[rbp-0F8]
M00_L45:
       mov       edx,[rbp-0E0]
       and       edx,7FFFFFFF
       mov       ecx,[rbp-0DC]
       mov       r8d,ecx
       add       r8,rdx
       mov       r11d,r14d
       cmp       r8,r11
       ja        near ptr M00_L51
       add       rbx,rdx
       mov       r14d,ecx
       jmp       near ptr M00_L18
M00_L46:
       lea       rdx,[rbp-0D8]
       mov       rcx,rdi
       mov       r11,7FFC542A05B0
       call      qword ptr [r11]
       jmp       near ptr M00_L19
M00_L47:
       mov       eax,[rbp-3C]
       mov       [rsp+20],eax
       mov       [rsp+28],ecx
       mov       [rsp+30],r8d
       mov       dword ptr [rsp+38],1
       mov       rcx,[rbp-168]
       mov       r8d,r11d
       call      qword ptr [7FFC54734138]
       jmp       near ptr M00_L10
M00_L48:
       mov       ecx,225
       mov       rdx,7FFC54713D90
       call      qword ptr [7FFC5435EE38]
       mov       rdx,rax
       mov       ecx,[rbp-64]
       call      qword ptr [7FFC54734108]
       int       3
M00_L49:
       call      qword ptr [7FFC54734180]
       int       3
M00_L50:
       mov       ecx,225
       mov       rdx,7FFC54713D90
       call      qword ptr [7FFC5435EE38]
       mov       rdx,rax
       mov       ecx,[rbp-0C4]
       call      qword ptr [7FFC54734108]
       int       3
M00_L51:
       call      qword ptr [7FFC54527990]
       int       3
M00_L52:
       mov       ecx,0A
       jmp       near ptr M00_L21
M00_L53:
       mov       ecx,0C
       mov       edx,0D
       call      qword ptr [7FFC546B5770]
       int       3
M00_L54:
       mov       ecx,225
       mov       rdx,7FFC54713D90
       call      qword ptr [7FFC5435EE38]
       mov       rdx,rax
       mov       ecx,[rbp-3C]
       call      qword ptr [7FFC54734108]
       int       3
M00_L55:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 1928
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
       jg        short M01_L00
       vmovdqu   xmm0,xmmword ptr [rcx+8]
       vmovdqu   xmmword ptr [rdx],xmm0
       mov       rax,rdx
       add       rsp,28
       ret
M01_L00:
       mov       edx,[rcx+14]
       mov       ecx,r8d
       call      qword ptr [7FFC546BFE58]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 48
```
```asm
; System.Text.Unicode.Utf8Utility.TranscodeToUtf8(Char*, Int32, Byte*, Int32, Char* ByRef, Byte* ByRef)
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       mov       r10,[rsp+48]
       mov       r11,[rsp+50]
       cmp       edx,r9d
       mov       eax,r9d
       cmovle    eax,edx
       xor       ebx,ebx
       cmp       rax,20
       jb        near ptr M02_L03
       mov       rsi,[rcx]
       mov       rdi,0FF80FF80FF80FF80
       test      rdi,rsi
       mov       rdi,rsi
       jne       near ptr M02_L18
       cmp       rax,40
       jb        near ptr M02_L10
       mov       rbx,rcx
       vmovups   ymm0,[rbx]
       vbroadcastss ymm1,dword ptr [7FFC543CEF30]
       vptest    ymm1,ymm0
       jne       near ptr M02_L08
       mov       rsi,r8
       vpackuswb ymm0,ymm0,ymm0
       vpermq    ymm0,ymm0,0D8
       vmovups   [rsi],xmm0
       mov       edi,10
       test      r8b,10
       jne       short M02_L00
       vmovups   ymm0,[rbx+20]
       vptest    ymm1,ymm0
       jne       short M02_L02
       vpackuswb ymm0,ymm0,ymm0
       vpermq    ymm0,ymm0,0D8
       vmovups   [rsi+10],xmm0
M02_L00:
       mov       rdi,r8
       and       rdi,1F
       neg       rdi
       add       rdi,20
       lea       rbp,[rax-20]
       vmovups   ymm0,[rbx+rdi*2]
       vmovups   ymm2,[rbx+rdi*2+20]
       vpor      ymm3,ymm0,ymm2
       vptest    ymm3,ymm1
       jne       near ptr M02_L09
M02_L01:
       vpackuswb ymm0,ymm0,ymm2
       vpermq    ymm2,ymm0,0D8
       vmovups   [rsi+rdi],ymm2
       add       rdi,20
       cmp       rdi,rbp
       ja        short M02_L02
       vmovups   ymm0,[rbx+rdi*2]
       vmovups   ymm2,[rbx+rdi*2+20]
       vpor      ymm3,ymm0,ymm2
       vptest    ymm3,ymm1
       jne       near ptr M02_L09
       jmp       short M02_L01
M02_L02:
       mov       rbx,rdi
M02_L03:
       sub       rax,rbx
       cmp       rax,4
       jb        short M02_L05
       lea       rsi,[rbx+rax-4]
       mov       rdi,[rcx+rbx*2]
       mov       rbp,0FF80FF80FF80FF80
       test      rbp,rdi
       jne       near ptr M02_L18
M02_L04:
       vmovq     xmm0,rdi
       vpackuswb xmm0,xmm0,xmm0
       vmovd     dword ptr [r8+rbx],xmm0
       add       rbx,4
       cmp       rbx,rsi
       ja        short M02_L05
       mov       rdi,[rcx+rbx*2]
       mov       rbp,0FF80FF80FF80FF80
       test      rbp,rdi
       jne       near ptr M02_L18
       jmp       short M02_L04
M02_L05:
       test      al,2
       je        short M02_L06
       mov       esi,[rcx+rbx*2]
       test      esi,0FF80FF80
       jne       near ptr M02_L19
       lea       rdi,[r8+rbx]
       mov       [rdi],sil
       shr       esi,10
       mov       [rdi+1],sil
       add       rbx,2
M02_L06:
       test      al,1
       jne       near ptr M02_L17
M02_L07:
       lea       rcx,[rcx+rbx*2]
       add       r8,rbx
       cmp       ebx,edx
       jne       near ptr M02_L22
       mov       [r10],rcx
       mov       [r11],r8
       xor       eax,eax
       vzeroupper
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M02_L08:
       xor       ebx,ebx
       jmp       near ptr M02_L03
M02_L09:
       vptest    ymm1,ymm0
       jne       near ptr M02_L02
       vpackuswb ymm0,ymm0,ymm0
       vpermq    ymm0,ymm0,0D8
       vmovups   [rsi+rdi],xmm0
       add       rdi,10
       jmp       near ptr M02_L02
M02_L10:
       mov       rbx,rcx
       vmovups   xmm0,[rbx]
       vptest    xmm0,xmmword ptr [7FFC543CEF40]
       je        short M02_L11
       xor       ebx,ebx
       jmp       near ptr M02_L16
M02_L11:
       mov       rsi,r8
       vpackuswb xmm0,xmm0,xmm0
       vmovsd    qword ptr [rsi],xmm0
       mov       edi,8
       test      r8b,8
       jne       short M02_L12
       vmovups   xmm0,[rbx+10]
       vptest    xmm0,xmmword ptr [7FFC543CEF40]
       jne       short M02_L14
       vpackuswb xmm0,xmm0,xmm0
       vmovsd    qword ptr [rsi+8],xmm0
M02_L12:
       mov       rdi,r8
       and       rdi,0F
       neg       rdi
       add       rdi,10
       lea       rbp,[rax-10]
M02_L13:
       vmovups   xmm0,[rbx+rdi*2]
       vmovups   xmm1,[rbx+rdi*2+10]
       vpor      xmm2,xmm0,xmm1
       vptest    xmm2,xmmword ptr [7FFC543CEF40]
       jne       short M02_L15
       vpackuswb xmm0,xmm0,xmm1
       vmovups   [rsi+rdi],xmm0
       add       rdi,10
       cmp       rdi,rbp
       jbe       short M02_L13
M02_L14:
       mov       rbx,rdi
       jmp       short M02_L16
M02_L15:
       vptest    xmm0,xmmword ptr [7FFC543CEF40]
       jne       short M02_L14
       vpackuswb xmm0,xmm0,xmm0
       vmovsd    qword ptr [rsi+rdi],xmm0
       add       rdi,8
       jmp       short M02_L14
M02_L16:
       jmp       near ptr M02_L03
M02_L17:
       movzx     esi,word ptr [rcx+rbx*2]
       cmp       esi,7F
       ja        near ptr M02_L07
       jmp       short M02_L20
M02_L18:
       mov       eax,edi
       test      eax,0FF80FF80
       jne       short M02_L21
       lea       rsi,[r8+rbx]
       mov       [rsi],al
       shr       eax,10
       mov       [rsi+1],al
       shr       rdi,20
       mov       eax,edi
       add       rbx,2
       mov       esi,eax
M02_L19:
       test      esi,0FF80
       jne       near ptr M02_L07
M02_L20:
       mov       [r8+rbx],sil
       inc       rbx
       jmp       near ptr M02_L07
M02_L21:
       mov       esi,eax
       jmp       short M02_L19
M02_L22:
       sub       edx,ebx
       sub       r9d,ebx
       cmp       edx,2
       jl        near ptr M02_L59
       mov       eax,edx
       lea       rax,[rcx+rax*2-4]
M02_L23:
       mov       ebx,[rcx]
       jmp       near ptr M02_L48
M02_L24:
       cmp       r9d,2
       jl        near ptr M02_L60
       mov       esi,ebx
       shr       esi,8
       or        esi,ebx
       mov       [r8],si
       add       rcx,4
       add       r8,2
       add       r9d,0FFFFFFFE
       mov       rbx,rax
       sub       rbx,rcx
       mov       rsi,rbx
       shr       rsi,3F
       add       rbx,rsi
       sar       rbx,1
       add       ebx,2
       movsxd    rsi,r9d
       cmp       rbx,rsi
       jle       short M02_L25
       jmp       short M02_L26
M02_L25:
       mov       rsi,rbx
M02_L26:
       mov       ebx,esi
       shr       ebx,3
       xor       edi,edi
       jmp       short M02_L28
M02_L27:
       vmovups   xmm0,[rcx]
       vptest    xmm0,xmmword ptr [7FFC543CEF40]
       jne       short M02_L29
       vpackuswb xmm0,xmm0,xmm0
       vmovq     qword ptr [r8],xmm0
       add       rcx,10
       add       r8,8
       inc       edi
M02_L28:
       cmp       edi,ebx
       jb        short M02_L27
       lea       ebx,[rdi*8]
       sub       r9d,ebx
       test      sil,4
       je        near ptr M02_L56
       mov       rbx,[rcx]
       mov       rsi,0FF80FF80FF80FF80
       test      rsi,rbx
       jne       short M02_L30
       jmp       near ptr M02_L51
M02_L29:
       shl       edi,3
       sub       r9d,edi
       vmovq     rbx,xmm0
       mov       rsi,0FF80FF80FF80FF80
       test      rsi,rbx
       jne       short M02_L30
       vpackuswb xmm1,xmm0,xmm0
       vmovd     dword ptr [r8],xmm1
       add       rcx,8
       add       r8,4
       add       r9d,0FFFFFFFC
       vpextrq   rbx,xmm0,1
M02_L30:
       mov       esi,ebx
       test      esi,0FF80FF80
       jne       short M02_L31
       mov       edi,esi
       shr       edi,8
       or        edi,esi
       mov       [r8],di
       add       rcx,4
       add       r8,2
       add       r9d,0FFFFFFFE
       shr       rbx,20
       mov       esi,ebx
M02_L31:
       test      esi,0FF80
       jne       short M02_L32
       test      r9d,r9d
       je        near ptr M02_L67
       jmp       short M02_L33
M02_L32:
       test      esi,0F800
       jne       near ptr M02_L49
       jmp       near ptr M02_L37
M02_L33:
       mov       [r8],sil
       add       rcx,2
       inc       r8
       dec       r9d
       cmp       rcx,rax
       ja        near ptr M02_L58
       mov       esi,[rcx]
       jmp       short M02_L32
M02_L34:
       cmp       r9d,2
       jl        near ptr M02_L67
       jmp       short M02_L38
M02_L35:
       cmp       r9d,4
       jl        short M02_L36
       mov       ebx,esi
       shr       ebx,6
       and       ebx,1F001F
       shl       esi,8
       and       esi,3F003F00
       add       ebx,esi
       add       ebx,80C080C0
       mov       [r8],ebx
       add       rcx,4
       add       r8,4
       add       r9d,0FFFFFFFC
       cmp       rcx,rax
       ja        near ptr M02_L58
       mov       esi,[rcx]
       lea       ebx,[rsi-80]
       movzx     ebx,bx
       cmp       ebx,780
       jl        short M02_L37
       mov       ebx,esi
       jmp       near ptr M02_L48
M02_L36:
       mov       ebx,esi
       jmp       near ptr M02_L60
M02_L37:
       lea       ebx,[rsi-800000]
       cmp       ebx,77FFFFF
       jbe       short M02_L35
       jmp       short M02_L34
M02_L38:
       lea       ebx,[rsi*4]
       and       ebx,1F00
       mov       edi,esi
       and       edi,3F
       lea       ebx,[rbx+rdi+0C080]
       movbe     [r8],bx
       cmp       esi,800000
       jb        short M02_L39
       add       rcx,2
       add       r8,2
       add       r9d,0FFFFFFFE
       cmp       rcx,rax
       ja        near ptr M02_L58
       jmp       short M02_L40
M02_L39:
       cmp       r9d,3
       jl        near ptr M02_L57
       jmp       near ptr M02_L52
M02_L40:
       mov       esi,[rcx]
       jmp       near ptr M02_L49
M02_L41:
       test      esi,0F8000000
       jne       short M02_L43
       jmp       short M02_L44
M02_L42:
       lea       ebx,[rsi+23FF2800]
       test      ebx,0FC00FC00
       je        near ptr M02_L54
       jmp       near ptr M02_L53
M02_L43:
       lea       ebx,[rsi+28000000]
       cmp       ebx,8000000
       jb        short M02_L44
       cmp       r9d,6
       jge       short M02_L45
M02_L44:
       cmp       r9d,3
       jl        near ptr M02_L67
       jmp       short M02_L46
M02_L45:
       lea       ebx,[rsi*4]
       and       ebx,3F00
       mov       edi,esi
       and       edi,3F
       shl       edi,10
       or        ebx,edi
       mov       edi,esi
       shr       edi,4
       and       edi,0F000000
       mov       ebp,esi
       shr       ebp,0C
       and       ebp,0F
       or        edi,ebp
       add       ebx,edi
       add       ebx,0E08080E0
       mov       [r8],ebx
       mov       ebx,esi
       shr       ebx,16
       and       ebx,3F
       shr       esi,8
       and       esi,3F00
       add       ebx,esi
       add       ebx,8080
       mov       [r8+4],bx
       add       rcx,4
       add       r8,6
       add       r9d,0FFFFFFFA
       cmp       rcx,rax
       ja        near ptr M02_L58
       mov       esi,[rcx]
       test      esi,0F800
       jne       near ptr M02_L49
       mov       ebx,esi
       jmp       short M02_L48
M02_L46:
       lea       ebx,[rsi*4]
       and       ebx,3F00
       movzx     edi,si
       shr       edi,0C
       add       ebx,edi
       add       ebx,80E0
       mov       [r8],bx
       mov       ebx,esi
       and       ebx,3F
       or        ebx,0FFFFFF80
       mov       [r8+2],bl
       add       rcx,2
       add       r8,3
       add       r9d,0FFFFFFFD
       cmp       esi,800000
       jb        short M02_L47
       cmp       rcx,rax
       ja        near ptr M02_L58
       jmp       short M02_L50
M02_L47:
       test      r9d,r9d
       je        near ptr M02_L67
       shr       esi,10
       mov       [r8],sil
       add       rcx,2
       inc       r8
       dec       r9d
       cmp       rcx,rax
       ja        near ptr M02_L58
       mov       esi,[rcx]
       test      esi,0F800
       jne       short M02_L49
       mov       ebx,esi
M02_L48:
       test      ebx,0FF80FF80
       je        near ptr M02_L24
       mov       esi,ebx
       jmp       near ptr M02_L31
M02_L49:
       lea       ebx,[rsi-0D800]
       test      ebx,0F800
       je        near ptr M02_L42
       jmp       near ptr M02_L41
M02_L50:
       mov       esi,[rcx]
       jmp       near ptr M02_L31
M02_L51:
       vmovq     xmm0,rbx
       vpackuswb xmm0,xmm0,xmm0
       vmovd     dword ptr [r8],xmm0
       add       rcx,8
       jmp       short M02_L55
M02_L52:
       shr       esi,10
       mov       [r8+2],sil
       add       rcx,4
       add       r8,3
       add       r9d,0FFFFFFFD
       jmp       short M02_L56
M02_L53:
       mov       eax,3
       jmp       near ptr M02_L68
M02_L54:
       cmp       r9d,4
       jl        near ptr M02_L67
       add       esi,40
       mov       ebx,esi
       and       ebx,3
       shl       ebx,14
       or        ebx,808080F0
       mov       edi,esi
       and       edi,3F0700
       bswap     edi
       rol       edi,10
       or        ebx,edi
       mov       edi,esi
       shr       edi,6
       and       edi,0F0000
       or        ebx,edi
       and       esi,0FC
       shl       esi,6
       or        ebx,esi
       mov       [r8],ebx
       add       rcx,4
M02_L55:
       add       r8,4
       add       r9d,0FFFFFFFC
M02_L56:
       cmp       rcx,rax
       jbe       near ptr M02_L23
       jmp       short M02_L58
M02_L57:
       add       rcx,2
       add       r8,2
       jmp       near ptr M02_L67
M02_L58:
       sub       rax,rcx
       mov       rdx,rax
       shr       rdx,3F
       add       rdx,rax
       sar       rdx,1
       add       edx,2
M02_L59:
       test      edx,edx
       je        near ptr M02_L66
       movzx     ebx,word ptr [rcx]
       jmp       short M02_L61
M02_L60:
       movzx     ebx,bx
M02_L61:
       cmp       ebx,7F
       ja        short M02_L62
       test      r9d,r9d
       je        near ptr M02_L67
       mov       [r8],bl
       add       rcx,2
       inc       r8
       jmp       near ptr M02_L65
M02_L62:
       cmp       ebx,800
       jae       short M02_L63
       cmp       r9d,2
       jl        near ptr M02_L67
       mov       r9d,ebx
       and       r9d,3F
       or        r9d,0FFFFFF80
       mov       [r8+1],r9b
       shr       ebx,6
       or        ebx,0FFFFFFC0
       mov       [r8],bl
       add       rcx,2
       add       r8,2
       jmp       short M02_L65
M02_L63:
       lea       eax,[rbx-0D800]
       cmp       eax,7FF
       jbe       short M02_L64
       cmp       r9d,3
       jl        short M02_L67
       mov       eax,ebx
       and       eax,3F
       or        eax,0FFFFFF80
       mov       [r8+2],al
       mov       eax,ebx
       shr       eax,6
       and       eax,3F
       or        eax,0FFFFFF80
       mov       [r8+1],al
       mov       eax,ebx
       shr       eax,0C
       or        eax,0FFFFFFE0
       mov       [r8],al
       add       rcx,2
       add       r8,3
       jmp       short M02_L65
M02_L64:
       cmp       ebx,0DBFF
       ja        near ptr M02_L53
       mov       eax,2
       jmp       short M02_L68
M02_L65:
       cmp       edx,1
       jg        short M02_L67
M02_L66:
       xor       eax,eax
       jmp       short M02_L68
M02_L67:
       mov       eax,1
M02_L68:
       mov       [r10],rcx
       mov       [r11],r8
       vzeroupper
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
; Total bytes of code 1985
```
```asm
; System.Text.Unicode.Utf16Utility.GetPointerToFirstInvalidChar(Char*, Int32, Int64 ByRef, Int32 ByRef)
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       mov       rax,rcx
       mov       r10d,edx
       mov       r11,rax
       cmp       r10,20
       jae       near ptr M03_L09
       cmp       r10,10
       jae       near ptr M03_L07
M03_L00:
       cmp       r10,4
       jb        short M03_L02
M03_L01:
       mov       r11d,[rax]
       mov       ebx,[rax+4]
       mov       esi,r11d
       or        esi,ebx
       test      esi,0FF80FF80
       jne       near ptr M03_L12
       add       rax,8
       add       r10,0FFFFFFFFFFFFFFFC
       cmp       r10,4
       jae       short M03_L01
M03_L02:
       test      r10b,2
       je        short M03_L03
       mov       r11d,[rax]
       test      r11d,0FF80FF80
       jne       near ptr M03_L13
       add       rax,4
M03_L03:
       test      r10b,1
       je        short M03_L05
       cmp       word ptr [rax],7F
       ja        short M03_L05
M03_L04:
       add       rax,2
M03_L05:
       sub       rax,rcx
       shr       rax,1
       mov       r10d,eax
       lea       rcx,[rcx+r10*2]
       sub       edx,eax
       jne       near ptr M03_L15
       xor       edx,edx
       mov       [r8],rdx
       mov       [r9],edx
M03_L06:
       mov       rax,rcx
       vzeroupper
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M03_L07:
       vbroadcastss xmm0,dword ptr [7FFC543CD420]
       vptest    xmm0,xmmword ptr [rax]
       jne       near ptr M03_L00
       lea       rbx,[r11+r10*2-10]
       add       r11,10
       mov       rax,r11
       and       rax,0FFFFFFFFFFFFFFF0
       vpand     xmm1,xmm0,[rax]
       vptest    xmm1,xmm1
       jne       short M03_L11
M03_L08:
       add       rax,10
       cmp       rax,rbx
       ja        short M03_L11
       vpand     xmm1,xmm0,[rax]
       vptest    xmm1,xmm1
       jne       short M03_L11
       jmp       short M03_L08
M03_L09:
       vbroadcastss ymm0,dword ptr [7FFC543CD420]
       vptest    ymm0,ymmword ptr [rax]
       jne       near ptr M03_L00
       lea       rbx,[r11+r10*2-20]
       lea       rax,[r11+20]
       and       rax,0FFFFFFFFFFFFFFE0
       vpand     ymm1,ymm0,[rax]
       vptest    ymm1,ymm1
       jne       short M03_L11
M03_L10:
       add       rax,20
       cmp       rax,rbx
       ja        short M03_L11
       vpand     ymm1,ymm0,[rax]
       vptest    ymm1,ymm1
       je        short M03_L10
M03_L11:
       mov       r11,rax
       sub       r11,rcx
       shr       r11,1
       sub       r10,r11
       jmp       near ptr M03_L00
M03_L12:
       test      r11d,0FF80FF80
       je        short M03_L14
M03_L13:
       test      r11d,0FF80
       jne       near ptr M03_L05
       jmp       near ptr M03_L04
M03_L14:
       mov       r11d,ebx
       add       rax,4
       jmp       short M03_L13
M03_L15:
       xor       eax,eax
       xor       r10d,r10d
       mov       r11d,edx
       lea       r11,[rcx+r11*2]
       cmp       edx,8
       jl        near ptr M03_L20
       vbroadcastss xmm0,dword ptr [7FFC543CD424]
       lea       rdx,[r11-10]
M03_L16:
       vmovups   xmm1,[rcx]
       add       rcx,10
       vpaddusw  xmm2,xmm1,xmm0
       vpminuw   xmm3,xmm1,[7FFC543CD430]
       vpor      xmm2,xmm2,xmm3
       vpmovmskb ebx,xmm2
       popcnt    ebx,ebx
       vpaddw    xmm2,xmm1,[7FFC543CD440]
       vpcmpgtw  xmm2,xmm0,xmm2
       vpmovmskb esi,xmm2
M03_L17:
       cmp       esi,0FFFF
       je        short M03_L19
       not       esi
       vpsrlw    xmm2,xmm1,3
       vpmovmskb edi,xmm2
       mov       ebp,edi
       and       ebp,esi
       xor       edi,5555
       and       esi,edi
       shl       esi,2
       movzx     edi,si
       cmp       edi,ebp
       jne       near ptr M03_L22
       cmp       esi,0FFFF
       jbe       short M03_L18
       movzx     esi,si
       add       rbx,0FFFFFFFFFFFFFFFE
       add       rcx,0FFFFFFFFFFFFFFFE
M03_L18:
       popcnt    esi,esi
       sub       r10d,esi
       sub       rax,rsi
       sub       rax,rsi
       mov       esi,0FFFF
       jmp       short M03_L17
M03_L19:
       add       rax,rbx
       cmp       rcx,rdx
       jbe       near ptr M03_L16
M03_L20:
       cmp       rcx,r11
       jae       short M03_L23
       movzx     edx,word ptr [rcx]
       cmp       edx,7F
       jbe       short M03_L21
       lea       ebx,[rdx+1F800]
       shr       ebx,10
       add       rax,rbx
       add       edx,0FFFF2800
       cmp       edx,7FF
       ja        short M03_L21
       add       rax,0FFFFFFFFFFFFFFFE
       mov       rdx,r11
       sub       rdx,rcx
       cmp       rdx,4
       jb        short M03_L23
       mov       edx,[rcx]
       add       edx,23FF2800
       test      edx,0FC00FC00
       jne       short M03_L23
       dec       r10d
       add       rax,2
       add       rcx,2
M03_L21:
       add       rcx,2
       jmp       short M03_L20
M03_L22:
       add       rcx,0FFFFFFFFFFFFFFF0
       jmp       short M03_L20
M03_L23:
       mov       [r8],rax
       mov       [r9],r10d
       jmp       near ptr M03_L06
; Total bytes of code 628
```
```asm
; Bshox.Internals.EncodingHelper.Utf8Encode(System.ReadOnlySpan`1<Char>, Byte ByRef, Int32)
;     {
;     ^
;         fixed (char* charsPtr = &MemoryMarshal.GetReference(chars))
;                ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         fixed (byte* bytesPtr = &bytes)
;                ^^^^^^^^^^^^^^^^^^^^^^^
;             return Utf8NoBom.GetBytes(charsPtr, chars.Length, bytesPtr, byteCount);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       sub       rsp,38
       mov       r9,[rcx]
       mov       [rsp+30],r9
       mov       rax,r9
       mov       [rsp+28],rdx
       mov       r9d,[rcx+8]
       mov       [rsp+20],r8d
       mov       r8d,r9d
       mov       r9,rdx
       mov       rcx,20CCE001300
       mov       rcx,[rcx]
       mov       rdx,rax
       call      qword ptr [7FFC5445CCA8]; System.Text.UTF8Encoding.GetBytes(Char*, Int32, Byte*, Int32)
       nop
       add       rsp,38
       ret
; Total bytes of code 63
```