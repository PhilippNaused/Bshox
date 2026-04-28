## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: DefaultJob)

```asm
; Benchmark.WriteVarInt.WriteByte()
       push      r15
       push      r14
       push      r13
       push      r12
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,58
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqu   ymmword ptr [rsp+30],ymm4
       mov       [rsp+50],rax
       mov       rsi,rcx
       mov       rdi,[rsi+38]
       xor       ebx,ebx
       xor       ebp,ebp
       xor       r14d,r14d
       mov       rcx,25530C012F8
       mov       r15,[rcx]
       xor       r13d,r13d
M00_L00:
       mov       rcx,[rsi+8]
       cmp       r13d,[rcx+8]
       jae       near ptr M00_L13
       movzx     r12d,byte ptr [rcx+r13*4+10]
       test      ebp,ebp
       jle       short M00_L02
M00_L01:
       mov       [rbx],r12b
       inc       rbx
       dec       ebp
       inc       r14d
       inc       r13d
       cmp       r13d,3E8
       jl        short M00_L00
       mov       rdi,[rsi+38]
       lea       rsi,[rdi+18]
       add       rdi,8
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,r14d
       add       rsp,58
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r12
       pop       r13
       pop       r14
       pop       r15
       ret
M00_L02:
       test      r14d,r14d
       jg        near ptr M00_L08
M00_L03:
       mov       r8d,[r15+0C]
       cmp       r8d,1
       jle       near ptr M00_L09
M00_L04:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rdi],rdx
       jne       near ptr M00_L11
       lea       rdx,[rsp+38]
       mov       rcx,rdi
       call      qword ptr [7FFD705E6278]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       ebx,ebx
       xor       ebp,ebp
       mov       rcx,[rsp+38]
       test      rcx,rcx
       je        short M00_L06
       mov       rdx,[rcx]
       test      dword ptr [rdx],80000000
       je        short M00_L10
       lea       rbx,[rcx+10]
       mov       ebp,[rcx+8]
M00_L05:
       mov       edx,[rsp+40]
       and       edx,7FFFFFFF
       mov       ecx,[rsp+44]
       mov       r8d,ecx
       add       r8,rdx
       mov       r11d,ebp
       cmp       r8,r11
       ja        near ptr M00_L12
       add       rbx,rdx
       mov       ebp,ecx
M00_L06:
       mov       [rsp+48],rbx
       mov       [rsp+50],ebp
M00_L07:
       cmp       dword ptr [rsp+50],0
       jbe       short M00_L13
       mov       rbx,[rsp+48]
       mov       ebp,[rsp+50]
       jmp       near ptr M00_L01
M00_L08:
       mov       rcx,rdi
       mov       edx,r14d
       mov       r11,7FFD702D04F8
       call      qword ptr [r11]
       xor       r14d,r14d
       jmp       near ptr M00_L03
M00_L09:
       mov       r8d,1
       jmp       near ptr M00_L04
M00_L10:
       lea       rdx,[rsp+28]
       mov       rax,[rcx]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rbx,[rsp+28]
       mov       ebp,[rsp+30]
       jmp       near ptr M00_L05
M00_L11:
       lea       rdx,[rsp+48]
       mov       rcx,rdi
       mov       r11,7FFD702D04F0
       call      qword ptr [r11]
       jmp       short M00_L07
M00_L12:
       call      qword ptr [7FFD70557990]
       int       3
M00_L13:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 412
```
```asm
; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
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
       call      qword ptr [7FFD706EFDE0]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 48
```

## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: DefaultJob)

```asm
; Benchmark.WriteVarInt.Write1()
       push      r15
       push      r14
       push      r13
       push      r12
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,58
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqu   ymmword ptr [rsp+30],ymm4
       mov       [rsp+50],rax
       mov       rsi,rcx
       mov       rdi,[rsi+38]
       xor       ebx,ebx
       xor       ebp,ebp
       xor       r14d,r14d
       mov       rcx,1C7C98012F8
       mov       r15,[rcx]
       xor       r13d,r13d
M00_L00:
       mov       rcx,[rsi+8]
       cmp       r13d,[rcx+8]
       jae       near ptr M00_L16
       mov       r12d,[rcx+r13*4+10]
       cmp       ebp,5
       jl        short M00_L03
M00_L01:
       xor       eax,eax
       cmp       r12d,7F
       ja        near ptr M00_L13
M00_L02:
       movsxd    rcx,eax
       mov       [rbx+rcx],r12b
       lea       r12d,[rax+1]
       test      r12d,r12d
       jl        near ptr M00_L15
       mov       eax,r12d
       add       rbx,rax
       sub       ebp,r12d
       add       r14d,r12d
       inc       r13d
       cmp       r13d,3E8
       jl        short M00_L00
       mov       rdi,[rsi+38]
       lea       rsi,[rdi+18]
       add       rdi,8
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,r14d
       add       rsp,58
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r12
       pop       r13
       pop       r14
       pop       r15
       ret
M00_L03:
       test      r14d,r14d
       jg        near ptr M00_L09
M00_L04:
       mov       r8d,[r15+0C]
       cmp       r8d,5
       jle       near ptr M00_L10
M00_L05:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rdi],rdx
       jne       near ptr M00_L12
       lea       rdx,[rsp+38]
       mov       rcx,rdi
       call      qword ptr [7FFD70606278]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       ebx,ebx
       xor       ebp,ebp
       mov       rcx,[rsp+38]
       test      rcx,rcx
       je        short M00_L07
       mov       rdx,[rcx]
       test      dword ptr [rdx],80000000
       je        short M00_L11
       lea       rbx,[rcx+10]
       mov       ebp,[rcx+8]
M00_L06:
       mov       edx,[rsp+40]
       and       edx,7FFFFFFF
       mov       ecx,[rsp+44]
       mov       r8d,ecx
       add       r8,rdx
       mov       r11d,ebp
       cmp       r8,r11
       ja        near ptr M00_L14
       add       rbx,rdx
       mov       ebp,ecx
M00_L07:
       mov       [rsp+48],rbx
       mov       [rsp+50],ebp
M00_L08:
       cmp       dword ptr [rsp+50],0
       jbe       near ptr M00_L16
       mov       rbx,[rsp+48]
       mov       ebp,[rsp+50]
       jmp       near ptr M00_L01
M00_L09:
       mov       rcx,rdi
       mov       edx,r14d
       mov       r11,7FFD702F04F8
       call      qword ptr [r11]
       xor       r14d,r14d
       jmp       near ptr M00_L04
M00_L10:
       mov       r8d,5
       jmp       near ptr M00_L05
M00_L11:
       lea       rdx,[rsp+28]
       mov       rax,[rcx]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rbx,[rsp+28]
       mov       ebp,[rsp+30]
       jmp       near ptr M00_L06
M00_L12:
       lea       rdx,[rsp+48]
       mov       rcx,rdi
       mov       r11,7FFD702F04F0
       call      qword ptr [r11]
       jmp       short M00_L08
M00_L13:
       lea       ecx,[rax+1]
       mov       edx,r12d
       or        edx,0FFFFFF80
       cdqe
       mov       [rbx+rax],dl
       shr       r12d,7
       cmp       r12d,7F
       mov       eax,ecx
       ja        short M00_L13
       jmp       near ptr M00_L02
M00_L14:
       call      qword ptr [7FFD70577990]
       int       3
M00_L15:
       mov       ecx,225
       mov       rdx,7FFD70763568
       call      qword ptr [7FFD703AEE38]
       mov       rdx,rax
       mov       ecx,r12d
       call      qword ptr [7FFD7070FFA8]
       int       3
M00_L16:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 514
```
```asm
; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
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
       call      qword ptr [7FFD7070FDE0]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 48
```

## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: DefaultJob)

```asm
; Benchmark.WriteVarInt.WriteAny()
       push      r15
       push      r14
       push      r13
       push      r12
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,58
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqu   ymmword ptr [rsp+30],ymm4
       mov       [rsp+50],rax
       mov       rsi,rcx
       mov       rdi,[rsi+38]
       xor       ebx,ebx
       xor       ebp,ebp
       xor       r14d,r14d
       mov       rcx,20E52C012F8
       mov       r15,[rcx]
       xor       r13d,r13d
       jmp       near ptr M00_L09
M00_L00:
       test      r14d,r14d
       jg        near ptr M00_L14
M00_L01:
       mov       r8d,[r15+0C]
       cmp       r8d,5
       jg        short M00_L02
       mov       r8d,5
M00_L02:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rdi],rdx
       jne       near ptr M00_L15
       lea       rdx,[rsp+38]
       mov       rcx,rdi
       call      qword ptr [7FFD705E6278]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       ebx,ebx
       xor       ebp,ebp
       mov       rcx,[rsp+38]
       test      rcx,rcx
       je        short M00_L05
       mov       rax,[rcx]
       mov       rdx,rax
       test      dword ptr [rdx],80000000
       jne       short M00_L03
       lea       rdx,[rsp+28]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       ebp,[rsp+30]
       mov       rbx,[rsp+28]
       jmp       short M00_L04
       nop       dword ptr [rax+rax]
       nop       dword ptr [rax+rax]
M00_L03:
       lea       rbx,[rcx+10]
       mov       ebp,[rcx+8]
M00_L04:
       mov       edx,[rsp+40]
       and       edx,7FFFFFFF
       mov       ecx,[rsp+44]
       mov       r8d,ecx
       add       r8,rdx
       mov       r11d,ebp
       cmp       r8,r11
       ja        near ptr M00_L13
       add       rbx,rdx
       mov       ebp,ecx
M00_L05:
       mov       [rsp+48],rbx
       mov       [rsp+50],ebp
M00_L06:
       cmp       dword ptr [rsp+50],0
       jbe       near ptr M00_L17
       mov       rbx,[rsp+48]
       mov       ebp,[rsp+50]
       jmp       short M00_L10
M00_L07:
       mov       ecx,eax
M00_L08:
       movsxd    rax,ecx
       mov       [rbx+rax],r12b
       lea       r12d,[rcx+1]
       test      r12d,r12d
       jl        near ptr M00_L16
       mov       eax,r12d
       add       rbx,rax
       sub       ebp,r12d
       add       r14d,r12d
       inc       r13d
       cmp       r13d,3E8
       jge       short M00_L12
M00_L09:
       mov       rcx,[rsi+30]
       cmp       r13d,[rcx+8]
       jae       near ptr M00_L17
       mov       r12d,[rcx+r13*4+10]
       cmp       ebp,5
       jl        near ptr M00_L00
M00_L10:
       xor       eax,eax
       cmp       r12d,7F
       jbe       short M00_L07
M00_L11:
       lea       ecx,[rax+1]
       mov       edx,r12d
       or        edx,0FFFFFF80
       cdqe
       mov       [rbx+rax],dl
       shr       r12d,7
       cmp       r12d,7F
       jbe       short M00_L08
       mov       eax,ecx
       jmp       short M00_L11
M00_L12:
       mov       rdi,[rsi+38]
       lea       rsi,[rdi+18]
       add       rdi,8
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,r14d
       add       rsp,58
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r12
       pop       r13
       pop       r14
       pop       r15
       ret
M00_L13:
       call      qword ptr [7FFD70557990]
       int       3
M00_L14:
       mov       rcx,rdi
       mov       edx,r14d
       mov       r11,7FFD702D04F8
       call      qword ptr [r11]
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L15:
       lea       rdx,[rsp+48]
       mov       rcx,rdi
       mov       r11,7FFD702D04F0
       call      qword ptr [r11]
       jmp       near ptr M00_L06
M00_L16:
       mov       ecx,225
       mov       rdx,7FFD70743128
       call      qword ptr [7FFD7038EE38]
       mov       rdx,rax
       mov       ecx,r12d
       call      qword ptr [7FFD706EFFA8]
       int       3
M00_L17:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 519
```
```asm
; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
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
       call      qword ptr [7FFD706EFD20]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 48
```