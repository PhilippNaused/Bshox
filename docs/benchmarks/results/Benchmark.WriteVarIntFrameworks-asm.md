## .NET 8.0.26 (8.0.26, 8.0.2626.16921), X64 RyuJIT x86-64-v3 (Job: net8.0-x64(Platform=X64, Runtime=.NET 8.0))

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
       sub       rsp,68
       vzeroupper
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+20],xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       vmovdqa   xmmword ptr [rsp+40],xmm4
       vmovdqa   xmmword ptr [rsp+50],xmm4
       xor       eax,eax
       mov       [rsp+60],rax
       mov       rbx,rcx
       mov       rax,[rbx+38]
       mov       [rsp+40],rax
       mov       rax,17094404F90
       mov       rax,[rax]
       mov       [rsp+48],rax
       xor       ebp,ebp
M00_L00:
       mov       rax,[rbx+8]
       cmp       ebp,[rax+8]
       jae       near ptr M00_L14
       mov       ecx,ebp
       movzx     r14d,byte ptr [rax+rcx*4+10]
       vxorps    xmm0,xmm0,xmm0
       vmovdqu   xmmword ptr [rsp+30],xmm0
       cmp       dword ptr [rsp+58],0
       jle       short M00_L02
M00_L01:
       mov       rax,[rsp+50]
       mov       [rax],r14b
       mov       rax,[rsp+50]
       inc       rax
       mov       [rsp+50],rax
       mov       eax,[rsp+58]
       dec       eax
       mov       [rsp+58],eax
       mov       eax,[rsp+5C]
       inc       eax
       mov       [rsp+5C],eax
       inc       ebp
       cmp       ebp,3E8
       jl        short M00_L00
       jmp       near ptr M00_L07
M00_L02:
       cmp       dword ptr [rsp+5C],0
       jg        near ptr M00_L10
       mov       rcx,[rsp+40]
       mov       rax,[rsp+48]
       mov       r8d,[rax+0C]
       cmp       r8d,1
       jle       near ptr M00_L11
M00_L03:
       mov       rax,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rcx],rax
       jne       near ptr M00_L13
       mov       edx,[rcx+14]
       mov       r15d,edx
       cmp       r8d,r15d
       jg        near ptr M00_L08
       mov       rax,[rcx+8]
       mov       r13d,[rcx+10]
       xor       r12d,r12d
       xor       ecx,ecx
       test      rax,rax
       je        short M00_L05
       mov       rcx,[rax]
       test      dword ptr [rcx],80000000
       je        near ptr M00_L12
       lea       r12,[rax+10]
       mov       ecx,[rax+8]
M00_L04:
       and       r13d,7FFFFFFF
       mov       eax,r13d
       mov       edx,r15d
       add       rdx,rax
       mov       ecx,ecx
       cmp       rdx,rcx
       ja        short M00_L09
       add       r12,rax
       mov       ecx,r15d
M00_L05:
       mov       rax,r12
       mov       [rsp+30],rax
       mov       [rsp+38],ecx
M00_L06:
       cmp       dword ptr [rsp+38],0
       jbe       near ptr M00_L14
       mov       rax,[rsp+30]
       mov       [rsp+50],rax
       mov       eax,[rsp+38]
       mov       [rsp+58],eax
       jmp       near ptr M00_L01
M00_L07:
       mov       rdi,[rbx+38]
       lea       rsi,[rdi+18]
       add       rdi,8
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,[rsp+5C]
       add       rsp,68
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r12
       pop       r13
       pop       r14
       pop       r15
       ret
M00_L08:
       mov       edx,r15d
       mov       ecx,r8d
       call      qword ptr [7FFD9502D488]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
M00_L09:
       call      qword ptr [7FFD94FBC4E0]
       int       3
M00_L10:
       mov       rcx,[rsp+40]
       mov       edx,[rsp+5C]
       mov       r11,7FFD94CE04C0
       call      qword ptr [r11]
       xor       edx,edx
       mov       [rsp+50],rdx
       mov       [rsp+58],edx
       mov       [rsp+5C],edx
       mov       rcx,[rsp+40]
       mov       rdx,[rsp+48]
       mov       r8d,[rdx+0C]
       cmp       r8d,1
       jg        near ptr M00_L03
M00_L11:
       mov       r8d,1
       jmp       near ptr M00_L03
M00_L12:
       lea       rdx,[rsp+20]
       mov       rcx,rax
       mov       rax,[rax]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       r12,[rsp+20]
       mov       ecx,[rsp+28]
       jmp       near ptr M00_L04
M00_L13:
       lea       rdx,[rsp+30]
       mov       r11,7FFD94CE04B8
       call      qword ptr [r11]
       jmp       near ptr M00_L06
M00_L14:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 564
```

## .NET 8.0.26 (8.0.26, 8.0.2626.16921), X64 RyuJIT x86-64-v3 (Job: net8.0-x64(Platform=X64, Runtime=.NET 8.0))

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
       sub       rsp,68
       vzeroupper
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+20],xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       vmovdqa   xmmword ptr [rsp+40],xmm4
       vmovdqa   xmmword ptr [rsp+50],xmm4
       xor       eax,eax
       mov       [rsp+60],rax
       mov       rbx,rcx
       mov       rax,[rbx+38]
       mov       [rsp+40],rax
       mov       rax,225D2C04F90
       mov       rax,[rax]
       mov       [rsp+48],rax
       xor       ebp,ebp
M00_L00:
       mov       rax,[rbx+8]
       cmp       ebp,[rax+8]
       jae       near ptr M00_L17
       mov       ecx,ebp
       mov       r14d,[rax+rcx*4+10]
       vxorps    xmm0,xmm0,xmm0
       vmovdqu   xmmword ptr [rsp+30],xmm0
       cmp       dword ptr [rsp+58],5
       jl        short M00_L03
       mov       r15,[rsp+50]
       mov       [r15],r14b
       cmp       r14d,7F
       ja        near ptr M00_L08
M00_L01:
       mov       rax,[rsp+50]
       inc       rax
       mov       [rsp+50],rax
       mov       eax,[rsp+58]
       dec       eax
       mov       [rsp+58],eax
       mov       eax,[rsp+5C]
       inc       eax
       mov       [rsp+5C],eax
M00_L02:
       inc       ebp
       cmp       ebp,3E8
       jl        short M00_L00
       mov       rdi,[rbx+38]
       lea       rsi,[rdi+18]
       add       rdi,8
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,[rsp+5C]
       add       rsp,68
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
       cmp       dword ptr [rsp+5C],0
       jg        near ptr M00_L13
       mov       rcx,[rsp+40]
       mov       rax,[rsp+48]
       mov       r8d,[rax+0C]
       cmp       r8d,5
       jle       near ptr M00_L14
M00_L04:
       mov       rax,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rcx],rax
       jne       near ptr M00_L16
       mov       edx,[rcx+14]
       mov       r15d,edx
       cmp       r8d,r15d
       jg        near ptr M00_L10
       mov       rax,[rcx+8]
       mov       r13d,[rcx+10]
       xor       r12d,r12d
       xor       ecx,ecx
       test      rax,rax
       je        short M00_L06
       mov       rcx,[rax]
       test      dword ptr [rcx],80000000
       je        near ptr M00_L15
       lea       r12,[rax+10]
       mov       ecx,[rax+8]
M00_L05:
       and       r13d,7FFFFFFF
       mov       eax,r13d
       mov       edx,r15d
       add       rdx,rax
       mov       ecx,ecx
       cmp       rdx,rcx
       ja        near ptr M00_L11
       add       r12,rax
       mov       ecx,r15d
M00_L06:
       mov       rax,r12
       mov       [rsp+30],rax
       mov       [rsp+38],ecx
M00_L07:
       cmp       dword ptr [rsp+38],0
       jbe       near ptr M00_L17
       mov       rax,[rsp+30]
       mov       [rsp+50],rax
       mov       eax,[rsp+38]
       mov       [rsp+58],eax
       mov       r15,[rsp+50]
       mov       [r15],r14b
       cmp       r14d,7F
       jbe       near ptr M00_L01
M00_L08:
       xor       edx,edx
M00_L09:
       lea       ecx,[rdx+1]
       mov       eax,r14d
       or        eax,80
       movsxd    rdx,edx
       mov       [r15+rdx],al
       shr       r14d,7
       cmp       r14d,7F
       mov       edx,ecx
       ja        short M00_L09
       movsxd    rcx,edx
       mov       [r15+rcx],r14b
       lea       esi,[rdx+1]
       test      esi,esi
       jl        short M00_L12
       movsxd    rdx,esi
       add       rdx,[rsp+50]
       mov       [rsp+50],rdx
       mov       edx,[rsp+58]
       sub       edx,esi
       mov       [rsp+58],edx
       add       esi,[rsp+5C]
       mov       [rsp+5C],esi
       jmp       near ptr M00_L02
M00_L10:
       mov       edx,r15d
       mov       ecx,r8d
       call      qword ptr [7FFD9504D488]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
M00_L11:
       call      qword ptr [7FFD94FDC4E0]
       int       3
M00_L12:
       mov       ecx,225
       mov       rdx,7FFD952485C8
       call      CORINFO_HELP_STRCNS
       mov       rdx,rax
       mov       ecx,esi
       call      qword ptr [7FFD9523F4E0]
       int       3
M00_L13:
       mov       rcx,[rsp+40]
       mov       edx,[rsp+5C]
       mov       r11,7FFD94D004C0
       call      qword ptr [r11]
       xor       edx,edx
       mov       [rsp+50],rdx
       mov       [rsp+58],edx
       mov       [rsp+5C],edx
       mov       rcx,[rsp+40]
       mov       rdx,[rsp+48]
       mov       r8d,[rdx+0C]
       cmp       r8d,5
       jg        near ptr M00_L04
M00_L14:
       mov       r8d,5
       jmp       near ptr M00_L04
M00_L15:
       lea       rdx,[rsp+20]
       mov       rcx,rax
       mov       rax,[rax]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       r12,[rsp+20]
       mov       ecx,[rsp+28]
       jmp       near ptr M00_L05
M00_L16:
       lea       rdx,[rsp+30]
       mov       r11,7FFD94D004B8
       call      qword ptr [r11]
       jmp       near ptr M00_L07
M00_L17:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 699
```

## .NET 8.0.26 (8.0.26, 8.0.2626.16921), X64 RyuJIT x86-64-v3 (Job: net8.0-x64(Platform=X64, Runtime=.NET 8.0))

```asm
; Benchmark.WriteVarInt.WriteAny()
       push      r15
       push      r14
       push      r13
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,70
       vzeroupper
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       vmovdqa   xmmword ptr [rsp+40],xmm4
       vmovdqa   xmmword ptr [rsp+50],xmm4
       vmovdqa   xmmword ptr [rsp+60],xmm4
       mov       rbx,rcx
       mov       rax,[rbx+38]
       mov       [rsp+48],rax
       mov       rax,26CA4004F90
       mov       rax,[rax]
       mov       [rsp+50],rax
       xor       ebp,ebp
M00_L00:
       mov       rax,[rbx+30]
       cmp       ebp,[rax+8]
       jae       near ptr M00_L18
       mov       ecx,ebp
       mov       r14d,[rax+rcx*4+10]
       vxorps    xmm0,xmm0,xmm0
       vmovdqu   xmmword ptr [rsp+38],xmm0
       cmp       dword ptr [rsp+60],5
       jl        near ptr M00_L06
M00_L01:
       mov       rax,[rsp+58]
       mov       [rax],r14b
       cmp       r14d,7F
       jbe       near ptr M00_L04
       xor       ecx,ecx
       nop       dword ptr [rax]
       nop       dword ptr [rax+rax]
M00_L02:
       lea       edx,[rcx+1]
       mov       r8d,r14d
       or        r8d,80
       movsxd    rcx,ecx
       mov       [rax+rcx],r8b
       shr       r14d,7
       cmp       r14d,7F
       mov       ecx,edx
       ja        short M00_L02
       movsxd    rdx,ecx
       mov       [rax+rdx],r14b
       lea       r15d,[rcx+1]
       test      r15d,r15d
       jl        near ptr M00_L15
       movsxd    rax,r15d
       add       rax,[rsp+58]
       mov       [rsp+58],rax
       mov       eax,[rsp+60]
       sub       eax,r15d
       mov       [rsp+60],eax
       add       r15d,[rsp+64]
       mov       [rsp+64],r15d
M00_L03:
       inc       ebp
       cmp       ebp,3E8
       jl        near ptr M00_L00
       mov       rdi,[rbx+38]
       lea       rsi,[rdi+18]
       add       rdi,8
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,[rsp+64]
       add       rsp,70
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r13
       pop       r14
       pop       r15
       ret
M00_L04:
       mov       rax,[rsp+58]
       inc       rax
       mov       [rsp+58],rax
       mov       eax,[rsp+60]
       dec       eax
       mov       [rsp+60],eax
       mov       eax,[rsp+64]
       inc       eax
       mov       [rsp+64],eax
       jmp       short M00_L03
M00_L05:
       mov       rcx,[rsp+48]
       mov       rdx,[rsp+50]
       mov       r8d,[rdx+0C]
       cmp       r8d,5
       jle       short M00_L07
       jmp       short M00_L08
M00_L06:
       cmp       dword ptr [rsp+64],0
       jg        near ptr M00_L16
       jmp       short M00_L05
M00_L07:
       mov       r8d,5
M00_L08:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rcx],rdx
       jne       near ptr M00_L17
       mov       edx,[rcx+14]
       mov       esi,edx
       cmp       r8d,esi
       jg        near ptr M00_L13
       mov       rdx,[rcx+8]
       mov       edi,[rcx+10]
       xor       r15d,r15d
       xor       r13d,r13d
       mov       rcx,rdx
       test      rcx,rcx
       je        short M00_L11
       mov       rdx,[rcx]
       test      dword ptr [rdx],80000000
       je        short M00_L09
       lea       r15,[rcx+10]
       mov       r13d,[rcx+8]
       jmp       short M00_L10
M00_L09:
       lea       rdx,[rsp+28]
       mov       rax,[rcx]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       r15,[rsp+28]
       mov       r13d,[rsp+30]
M00_L10:
       and       edi,7FFFFFFF
       mov       ecx,edi
       mov       edx,esi
       add       rdx,rcx
       mov       r11d,r13d
       cmp       rdx,r11
       ja        short M00_L14
       add       r15,rcx
       mov       r13d,esi
M00_L11:
       mov       rcx,r15
       mov       [rsp+38],rcx
       mov       [rsp+40],r13d
M00_L12:
       cmp       dword ptr [rsp+40],0
       jbe       near ptr M00_L18
       mov       rcx,[rsp+38]
       mov       [rsp+58],rcx
       mov       ecx,[rsp+40]
       mov       [rsp+60],ecx
       jmp       near ptr M00_L01
M00_L13:
       mov       edx,esi
       mov       ecx,r8d
       call      qword ptr [7FFD9505D488]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
M00_L14:
       call      qword ptr [7FFD94FEC4E0]
       int       3
M00_L15:
       mov       ecx,225
       mov       rdx,7FFD95258530
       call      CORINFO_HELP_STRCNS
       mov       rdx,rax
       mov       ecx,r15d
       call      qword ptr [7FFD9524F660]
       int       3
M00_L16:
       mov       rcx,[rsp+48]
       mov       edx,[rsp+64]
       mov       r11,7FFD94D104C0
       call      qword ptr [r11]
       xor       edx,edx
       mov       [rsp+58],rdx
       mov       [rsp+60],edx
       mov       [rsp+64],edx
       jmp       near ptr M00_L05
M00_L17:
       lea       rdx,[rsp+38]
       mov       r11,7FFD94D104B8
       call      qword ptr [r11]
       jmp       near ptr M00_L12
M00_L18:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 678
```

## .NET 9.0.15 (9.0.15, 9.0.1526.17522), X64 RyuJIT x86-64-v3 (Job: net9.0-x64(Platform=X64, Runtime=.NET 9.0))

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
       mov       rbx,rcx
       mov       rbp,[rbx+38]
       xor       r14d,r14d
       xor       r15d,r15d
       xor       r13d,r13d
       mov       rax,1A99D001320
       mov       r12,[rax]
       xor       esi,esi
M00_L00:
       mov       rax,[rbx+8]
       cmp       esi,[rax+8]
       jae       near ptr M00_L13
       movzx     edi,byte ptr [rax+rsi*4+10]
       test      r15d,r15d
       jle       short M00_L02
M00_L01:
       mov       [r14],dil
       inc       r14
       dec       r15d
       inc       r13d
       inc       esi
       cmp       esi,3E8
       jl        short M00_L00
       mov       rdi,[rbx+38]
       lea       rsi,[rdi+18]
       add       rdi,8
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,r13d
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
       test      r13d,r13d
       jg        near ptr M00_L08
M00_L03:
       mov       r8d,[r12+0C]
       cmp       r8d,1
       jle       near ptr M00_L09
M00_L04:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rbp],rdx
       jne       near ptr M00_L12
       lea       rdx,[rsp+38]
       mov       rcx,rbp
       call      qword ptr [7FFD95033400]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       ecx,ecx
       xor       edx,edx
       mov       rax,[rsp+38]
       test      rax,rax
       je        short M00_L06
       mov       rcx,[rax]
       test      dword ptr [rcx],80000000
       je        short M00_L10
       lea       rcx,[rax+10]
       mov       edx,[rax+8]
M00_L05:
       mov       r11d,[rsp+40]
       and       r11d,7FFFFFFF
       mov       eax,[rsp+44]
       mov       r8d,eax
       add       r8,r11
       mov       edx,edx
       cmp       r8,rdx
       ja        short M00_L11
       add       rcx,r11
       mov       edx,eax
M00_L06:
       mov       [rsp+48],rcx
       mov       [rsp+50],edx
M00_L07:
       cmp       dword ptr [rsp+50],0
       jbe       short M00_L13
       mov       r14,[rsp+48]
       mov       r15d,[rsp+50]
       jmp       near ptr M00_L01
M00_L08:
       mov       rcx,rbp
       mov       edx,r13d
       mov       r11,7FFD94CE04E0
       call      qword ptr [r11]
       xor       r13d,r13d
       jmp       near ptr M00_L03
M00_L09:
       mov       r8d,1
       jmp       near ptr M00_L04
M00_L10:
       lea       rdx,[rsp+28]
       mov       rcx,rax
       mov       rax,[rax]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rcx,[rsp+28]
       mov       edx,[rsp+30]
       jmp       near ptr M00_L05
M00_L11:
       call      qword ptr [7FFD94F873A8]
       int       3
M00_L12:
       lea       rdx,[rsp+48]
       mov       rcx,rbp
       mov       r11,7FFD94CE04D8
       call      qword ptr [r11]
       jmp       short M00_L07
M00_L13:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 415
```
```asm
; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       push      rdi
       push      rsi
       sub       rsp,28
       cmp       r8d,[rcx+14]
       jg        short M01_L00
       lea       rsi,[rcx+8]
       mov       rdi,rdx
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       rax,rdx
       add       rsp,28
       pop       rsi
       pop       rdi
       ret
M01_L00:
       mov       edx,[rcx+14]
       mov       ecx,r8d
       call      qword ptr [7FFD9513EE38]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 57
```

## .NET 9.0.15 (9.0.15, 9.0.1526.17522), X64 RyuJIT x86-64-v3 (Job: net9.0-x64(Platform=X64, Runtime=.NET 9.0))

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
       mov       rbx,rcx
       mov       rbp,[rbx+38]
       xor       r14d,r14d
       xor       r15d,r15d
       xor       r13d,r13d
       mov       rax,22D25401320
       mov       r12,[rax]
       xor       esi,esi
M00_L00:
       mov       rax,[rbx+8]
       cmp       esi,[rax+8]
       jae       near ptr M00_L17
       mov       edi,[rax+rsi*4+10]
       cmp       r15d,5
       jl        short M00_L03
M00_L01:
       mov       rax,r14
       mov       [rax],dil
       cmp       edi,7F
       ja        near ptr M00_L14
       inc       r14
       dec       r15d
       inc       r13d
M00_L02:
       inc       esi
       cmp       esi,3E8
       jl        short M00_L00
       mov       rdi,[rbx+38]
       lea       rsi,[rdi+18]
       add       rdi,8
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,r13d
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
       test      r13d,r13d
       jg        near ptr M00_L10
M00_L04:
       mov       r8d,[r12+0C]
       cmp       r8d,5
       jle       near ptr M00_L11
M00_L05:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rbp],rdx
       jne       near ptr M00_L13
       lea       rdx,[rsp+38]
       mov       rcx,rbp
       call      qword ptr [7FFD95053400]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       r14d,r14d
       xor       r15d,r15d
       mov       rcx,[rsp+38]
       test      rcx,rcx
       je        short M00_L08
       mov       rdx,[rcx]
       test      dword ptr [rdx],80000000
       je        short M00_L06
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
       jmp       short M00_L07
M00_L06:
       lea       rdx,[rsp+28]
       mov       rax,[rcx]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       r15d,[rsp+30]
       mov       r14,[rsp+28]
M00_L07:
       mov       ecx,[rsp+40]
       and       ecx,7FFFFFFF
       mov       edx,[rsp+44]
       mov       r11d,edx
       add       r11,rcx
       mov       eax,r15d
       cmp       r11,rax
       ja        short M00_L12
       add       r14,rcx
       mov       r15d,edx
M00_L08:
       mov       rcx,r14
       mov       [rsp+48],rcx
       mov       [rsp+50],r15d
M00_L09:
       cmp       dword ptr [rsp+50],0
       jbe       near ptr M00_L17
       mov       r14,[rsp+48]
       mov       r15d,[rsp+50]
       jmp       near ptr M00_L01
M00_L10:
       mov       rcx,rbp
       mov       edx,r13d
       mov       r11,7FFD94D004E0
       call      qword ptr [r11]
       xor       r13d,r13d
       jmp       near ptr M00_L04
M00_L11:
       mov       r8d,5
       jmp       near ptr M00_L05
M00_L12:
       call      qword ptr [7FFD94FA73A8]
       int       3
M00_L13:
       lea       rdx,[rsp+48]
       mov       rcx,rbp
       mov       r11,7FFD94D004D8
       call      qword ptr [r11]
       jmp       short M00_L09
M00_L14:
       xor       ecx,ecx
M00_L15:
       lea       edx,[rcx+1]
       mov       eax,edi
       or        eax,80
       movsxd    rcx,ecx
       mov       [r14+rcx],al
       shr       edi,7
       cmp       edi,7F
       mov       ecx,edx
       ja        short M00_L15
       movsxd    rdx,ecx
       mov       [r14+rdx],dil
       lea       edi,[rcx+1]
       test      edi,edi
       jl        short M00_L16
       movsxd    rcx,edi
       add       r14,rcx
       sub       r15d,edi
       add       r13d,edi
       jmp       near ptr M00_L02
M00_L16:
       mov       ecx,225
       mov       rdx,7FFD951E0FC8
       call      CORINFO_HELP_STRCNS
       mov       rdx,rax
       mov       ecx,edi
       call      qword ptr [7FFD9515F0D8]
       int       3
M00_L17:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 524
```
```asm
; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       push      rdi
       push      rsi
       sub       rsp,28
       cmp       r8d,[rcx+14]
       jg        short M01_L00
       lea       rsi,[rcx+8]
       mov       rdi,rdx
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       rax,rdx
       add       rsp,28
       pop       rsi
       pop       rdi
       ret
M01_L00:
       mov       edx,[rcx+14]
       mov       ecx,r8d
       call      qword ptr [7FFD9515EF28]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 57
```

## .NET 9.0.15 (9.0.15, 9.0.1526.17522), X64 RyuJIT x86-64-v3 (Job: net9.0-x64(Platform=X64, Runtime=.NET 9.0))

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
       mov       rbx,rcx
       mov       rbp,[rbx+38]
       xor       r14d,r14d
       xor       r15d,r15d
       xor       r13d,r13d
       mov       rax,200ED401320
       mov       r12,[rax]
       xor       esi,esi
M00_L00:
       mov       rax,[rbx+30]
       cmp       esi,[rax+8]
       jae       near ptr M00_L17
       mov       edi,[rax+rsi*4+10]
       cmp       r15d,5
       jl        near ptr M00_L04
M00_L01:
       mov       rax,r14
       mov       [rax],dil
       cmp       edi,7F
       jbe       near ptr M00_L10
       xor       eax,eax
       nop       dword ptr [rax]
       nop       dword ptr [rax+rax]
M00_L02:
       lea       ecx,[rax+1]
       mov       edx,edi
       or        edx,80
       cdqe
       mov       [r14+rax],dl
       shr       edi,7
       cmp       edi,7F
       mov       eax,ecx
       ja        short M00_L02
       movsxd    rcx,eax
       mov       [r14+rcx],dil
       lea       edi,[rax+1]
       test      edi,edi
       jl        near ptr M00_L16
       movsxd    rax,edi
       add       r14,rax
       sub       r15d,edi
       add       r13d,edi
M00_L03:
       inc       esi
       cmp       esi,3E8
       jl        short M00_L00
       mov       rdi,[rbx+38]
       lea       rsi,[rdi+18]
       add       rdi,8
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,r13d
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
M00_L04:
       test      r13d,r13d
       jg        near ptr M00_L11
M00_L05:
       mov       r8d,[r12+0C]
       cmp       r8d,5
       jle       near ptr M00_L12
M00_L06:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rbp],rdx
       jne       near ptr M00_L15
       lea       rdx,[rsp+38]
       mov       rcx,rbp
       call      qword ptr [7FFD95063400]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       ecx,ecx
       xor       edx,edx
       mov       rax,[rsp+38]
       test      rax,rax
       je        short M00_L08
       mov       rcx,[rax]
       test      dword ptr [rcx],80000000
       je        near ptr M00_L13
       lea       rcx,[rax+10]
       mov       edx,[rax+8]
M00_L07:
       mov       r11d,[rsp+40]
       and       r11d,7FFFFFFF
       mov       eax,[rsp+44]
       mov       r8d,eax
       add       r8,r11
       mov       edx,edx
       cmp       r8,rdx
       ja        near ptr M00_L14
       add       rcx,r11
       mov       edx,eax
M00_L08:
       mov       [rsp+48],rcx
       mov       [rsp+50],edx
M00_L09:
       cmp       dword ptr [rsp+50],0
       jbe       near ptr M00_L17
       mov       r14,[rsp+48]
       mov       r15d,[rsp+50]
       jmp       near ptr M00_L01
M00_L10:
       inc       r14
       dec       r15d
       inc       r13d
       jmp       near ptr M00_L03
M00_L11:
       mov       rcx,rbp
       mov       edx,r13d
       mov       r11,7FFD94D104E0
       call      qword ptr [r11]
       xor       r13d,r13d
       jmp       near ptr M00_L05
M00_L12:
       mov       r8d,5
       jmp       near ptr M00_L06
M00_L13:
       lea       rdx,[rsp+28]
       mov       rcx,rax
       mov       rax,[rax]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rcx,[rsp+28]
       mov       edx,[rsp+30]
       jmp       near ptr M00_L07
M00_L14:
       call      qword ptr [7FFD94FB73A8]
       int       3
M00_L15:
       lea       rdx,[rsp+48]
       mov       rcx,rbp
       mov       r11,7FFD94D104D8
       call      qword ptr [r11]
       jmp       near ptr M00_L09
M00_L16:
       mov       ecx,225
       mov       rdx,7FFD951F1030
       call      CORINFO_HELP_STRCNS
       mov       rdx,rax
       mov       ecx,edi
       call      qword ptr [7FFD9516F0D8]
       int       3
M00_L17:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 556
```
```asm
; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       push      rdi
       push      rsi
       sub       rsp,28
       cmp       r8d,[rcx+14]
       jg        short M01_L00
       lea       rsi,[rcx+8]
       mov       rdi,rdx
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       rax,rdx
       add       rsp,28
       pop       rsi
       pop       rdi
       ret
M01_L00:
       mov       edx,[rcx+14]
       mov       ecx,r8d
       call      qword ptr [7FFD9516EF28]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 57
```

## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: net10.0-x64(Platform=X64, Runtime=.NET 10.0))

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
       mov       rcx,1E871001318
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
       call      qword ptr [7FFD705D7440]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
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
       mov       r11,7FFD702C0500
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
       mov       r11,7FFD702C04F8
       call      qword ptr [r11]
       jmp       short M00_L07
M00_L12:
       call      qword ptr [7FFD70547990]
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
       call      qword ptr [7FFD706DFF78]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 48
```

## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: net10.0-x64(Platform=X64, Runtime=.NET 10.0))

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
       mov       rcx,17E22401318
       mov       r15,[rcx]
       xor       r13d,r13d
M00_L00:
       mov       rcx,[rsi+8]
       cmp       r13d,[rcx+8]
       jae       near ptr M00_L17
       mov       r12d,[rcx+r13*4+10]
       cmp       ebp,5
       jl        short M00_L03
M00_L01:
       mov       rax,rbx
       mov       [rax],r12b
       cmp       r12d,7F
       ja        near ptr M00_L13
       inc       rbx
       dec       ebp
       inc       r14d
M00_L02:
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
       call      qword ptr [7FFD705E7440]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
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
       ja        near ptr M00_L15
       add       rbx,rdx
       mov       ebp,ecx
M00_L07:
       mov       [rsp+48],rbx
       mov       [rsp+50],ebp
M00_L08:
       cmp       dword ptr [rsp+50],0
       jbe       near ptr M00_L17
       mov       rbx,[rsp+48]
       mov       ebp,[rsp+50]
       jmp       near ptr M00_L01
M00_L09:
       mov       rcx,rdi
       mov       edx,r14d
       mov       r11,7FFD702D0500
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
       mov       r11,7FFD702D04F8
       call      qword ptr [r11]
       jmp       short M00_L08
M00_L13:
       xor       eax,eax
M00_L14:
       lea       ecx,[rax+1]
       mov       edx,r12d
       or        edx,80
       cdqe
       mov       [rbx+rax],dl
       shr       r12d,7
       cmp       r12d,7F
       mov       eax,ecx
       ja        short M00_L14
       movsxd    rcx,eax
       mov       [rbx+rcx],r12b
       lea       r12d,[rax+1]
       test      r12d,r12d
       jl        short M00_L16
       mov       eax,r12d
       add       rbx,rax
       sub       ebp,r12d
       add       r14d,r12d
       jmp       near ptr M00_L02
M00_L15:
       call      qword ptr [7FFD70557990]
       int       3
M00_L16:
       mov       ecx,225
       mov       rdx,7FFD70744AB0
       call      qword ptr [7FFD7038EE38]
       mov       rdx,rax
       mov       ecx,r12d
       call      qword ptr [7FFD70764180]
       int       3
M00_L17:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 527
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
       call      qword ptr [7FFD706EFEE8]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 48
```

## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: net10.0-x64(Platform=X64, Runtime=.NET 10.0))

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
       mov       rcx,24F65801318
       mov       r15,[rcx]
       xor       r13d,r13d
       jmp       near ptr M00_L08
M00_L00:
       test      r14d,r14d
       jg        near ptr M00_L13
M00_L01:
       mov       r8d,[r15+0C]
       cmp       r8d,5
       jle       near ptr M00_L14
M00_L02:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rdi],rdx
       jne       near ptr M00_L16
       lea       rdx,[rsp+38]
       mov       rcx,rdi
       call      qword ptr [7FFD705D7440]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       ebx,ebx
       xor       ebp,ebp
       mov       rcx,[rsp+38]
       test      rcx,rcx
       je        short M00_L04
       mov       rdx,[rcx]
       test      dword ptr [rdx],80000000
       je        near ptr M00_L15
       lea       rbx,[rcx+10]
       mov       ebp,[rcx+8]
M00_L03:
       mov       edx,[rsp+40]
       and       edx,7FFFFFFF
       mov       ecx,[rsp+44]
       mov       r8d,ecx
       add       r8,rdx
       mov       r11d,ebp
       cmp       r8,r11
       ja        near ptr M00_L17
       add       rbx,rdx
       mov       ebp,ecx
M00_L04:
       mov       [rsp+48],rbx
       mov       [rsp+50],ebp
M00_L05:
       cmp       dword ptr [rsp+50],0
       jbe       near ptr M00_L19
       mov       rbx,[rsp+48]
       mov       ebp,[rsp+50]
       jmp       short M00_L09
       nop
M00_L06:
       movsxd    rcx,eax
       mov       [rbx+rcx],r12b
       lea       r12d,[rax+1]
       test      r12d,r12d
       jl        near ptr M00_L18
       mov       eax,r12d
       add       rbx,rax
       sub       ebp,r12d
       add       r14d,r12d
M00_L07:
       inc       r13d
       cmp       r13d,3E8
       jge       short M00_L11
M00_L08:
       mov       rcx,[rsi+30]
       cmp       r13d,[rcx+8]
       jae       near ptr M00_L19
       mov       r12d,[rcx+r13*4+10]
       cmp       ebp,5
       jl        near ptr M00_L00
M00_L09:
       mov       rax,rbx
       mov       [rax],r12b
       cmp       r12d,7F
       jbe       short M00_L12
       xor       eax,eax
M00_L10:
       lea       ecx,[rax+1]
       mov       edx,r12d
       or        edx,80
       cdqe
       mov       [rbx+rax],dl
       shr       r12d,7
       cmp       r12d,7F
       mov       eax,ecx
       ja        short M00_L10
       jmp       short M00_L06
M00_L11:
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
M00_L12:
       inc       rbx
       dec       ebp
       inc       r14d
       jmp       near ptr M00_L07
M00_L13:
       mov       rcx,rdi
       mov       edx,r14d
       mov       r11,7FFD702C0500
       call      qword ptr [r11]
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L14:
       mov       r8d,5
       jmp       near ptr M00_L02
M00_L15:
       lea       rdx,[rsp+28]
       mov       rax,[rcx]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rbx,[rsp+28]
       mov       ebp,[rsp+30]
       jmp       near ptr M00_L03
M00_L16:
       lea       rdx,[rsp+48]
       mov       rcx,rdi
       mov       r11,7FFD702C04F8
       call      qword ptr [r11]
       jmp       near ptr M00_L05
M00_L17:
       call      qword ptr [7FFD70547990]
       int       3
M00_L18:
       mov       ecx,225
       mov       rdx,7FFD70734AB0
       call      qword ptr [7FFD7037EE38]
       mov       rdx,rax
       mov       ecx,r12d
       call      qword ptr [7FFD707540A8]
       int       3
M00_L19:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 543
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
       call      qword ptr [7FFD706DFEE8]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 48
```

## .NET Framework 4.8.1 (4.8.9325.0), X64 RyuJIT VectorSize=256 (Job: net48-x64(Platform=X64, Runtime=.NET Framework 4.8))

```asm
; Benchmark.WriteVarInt.WriteByte()
       push      rdi
       push      rsi
       sub       rsp,58
       vzeroupper
       mov       rsi,rcx
       lea       rdi,[rsp+28]
       mov       ecx,0C
       xor       eax,eax
       rep stosd
       mov       rcx,rsi
       mov       rsi,rcx
       mov       rdx,[rsi+38]
       xor       ecx,ecx
       mov       [rsp+38],ecx
       lea       rax,[rsp+40]
       vxorps    xmm0,xmm0,xmm0
       vmovdqu   xmmword ptr [rax],xmm0
       mov       [rax+10],rcx
       mov       [rsp+3C],ecx
       mov       [rsp+28],rdx
       lea       rdx,[rsp+28]
       mov       rcx,20097D1CC60
       mov       rax,[rcx]
       lea       rcx,[rdx+8]
       mov       rdx,rax
       call      CORINFO_HELP_CHECKED_ASSIGN_REF
       xor       edi,edi
M00_L00:
       lea       rcx,[rsp+28]
       mov       rdx,[rsi+8]
       cmp       edi,[rdx+8]
       jae       short M00_L01
       movsxd    rax,edi
       movzx     edx,byte ptr [rdx+rax*4+10]
       call      Bshox.BshoxWriter.WriteByte(Byte)
       inc       edi
       cmp       edi,3E8
       jl        short M00_L00
       mov       rax,[rsi+38]
       lea       rsi,[rax+18]
       lea       rdi,[rax+8]
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,[rsp+38]
       add       rsp,58
       pop       rsi
       pop       rdi
       ret
M00_L01:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 175
```
```asm
; Bshox.BshoxWriter.WriteByte(Byte)
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,20
       vzeroupper
       mov       rsi,rcx
       mov       edi,edx
       cmp       [rsi],esi
       lea       rbx,[rsi+18]
       mov       rcx,rbx
       mov       ecx,[rcx+10]
       mov       edx,[rsi+10]
       sub       ecx,edx
       test      ecx,ecx
       jle       short M01_L00
       cmp       [rsi],esi
       cmp       edx,[rbx+10]
       ja        near ptr M01_L07
       mov       r11,[rbx+8]
       movsxd    rdx,edx
       add       rdx,r11
       mov       ebp,ecx
       mov       rcx,[rbx]
       mov       r14,rcx
       mov       ecx,ebp
       mov       ebp,ecx
       jmp       short M01_L04
M01_L00:
       test      edx,edx
       jle       short M01_L01
       test      edx,edx
       je        short M01_L01
       mov       rcx,[rsi]
       mov       r11,7FFD9D4B0390
       mov       rax,7FFD9D4B0390
       cmp       [rcx],ecx
       call      qword ptr [rax]
       xor       edx,edx
       mov       [rsi+10],edx
       vxorps    xmm0,xmm0,xmm0
       vmovdqu   xmmword ptr [rbx],xmm0
       mov       [rbx+10],rdx
M01_L01:
       mov       rcx,[rsi]
       mov       rdx,[rsi+8]
       mov       edx,[rdx+0C]
       cmp       edx,1
       jle       short M01_L02
       mov       r8d,edx
       jmp       short M01_L03
M01_L02:
       mov       r8d,1
M01_L03:
       cmp       [rsi],esi
       mov       rdx,rbx
       mov       r11,7FFD9D4B0388
       mov       rax,7FFD9D4B0388
       cmp       [rcx],ecx
       call      qword ptr [rax]
       mov       rcx,rbx
       mov       r14,[rcx]
       mov       rdx,[rcx+8]
       mov       ebp,[rcx+10]
M01_L04:
       test      ebp,ebp
       jbe       short M01_L08
       test      r14,r14
       jne       short M01_L05
       mov       rcx,rdx
       mov       rdx,rcx
       mov       rcx,rdx
       jmp       short M01_L06
M01_L05:
       lea       rcx,[r14+8]
       add       rcx,rdx
M01_L06:
       mov       [rcx],dil
       inc       dword ptr [rsi+10]
       add       rsp,20
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M01_L07:
       mov       ecx,1
       call      00007FFD9D5CCB20
       int       3
M01_L08:
       call      00007FFD9D5CCB2C
       int       3
; Total bytes of code 257
```

## .NET Framework 4.8.1 (4.8.9325.0), X64 RyuJIT VectorSize=256 (Job: net48-x64(Platform=X64, Runtime=.NET Framework 4.8))

```asm
; Benchmark.WriteVarInt.Write1()
       push      rdi
       push      rsi
       sub       rsp,58
       vzeroupper
       mov       rsi,rcx
       lea       rdi,[rsp+28]
       mov       ecx,0C
       xor       eax,eax
       rep stosd
       mov       rcx,rsi
       mov       rsi,rcx
       mov       rdx,[rsi+38]
       xor       ecx,ecx
       mov       [rsp+38],ecx
       lea       rax,[rsp+40]
       vxorps    xmm0,xmm0,xmm0
       vmovdqu   xmmword ptr [rax],xmm0
       mov       [rax+10],rcx
       mov       [rsp+3C],ecx
       mov       [rsp+28],rdx
       lea       rdx,[rsp+28]
       mov       rcx,1E03273CC60
       mov       rax,[rcx]
       lea       rcx,[rdx+8]
       mov       rdx,rax
       call      CORINFO_HELP_CHECKED_ASSIGN_REF
       xor       edi,edi
M00_L00:
       lea       rcx,[rsp+28]
       mov       rdx,[rsi+8]
       cmp       edi,[rdx+8]
       jae       short M00_L01
       movsxd    rax,edi
       mov       edx,[rdx+rax*4+10]
       call      Bshox.BshoxWriter.WriteVarInt32(UInt32)
       inc       edi
       cmp       edi,3E8
       jl        short M00_L00
       mov       rax,[rsi+38]
       lea       rsi,[rax+18]
       lea       rdi,[rax+8]
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,[rsp+38]
       add       rsp,58
       pop       rsi
       pop       rdi
       ret
M00_L01:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 174
```
```asm
; Bshox.BshoxWriter.WriteVarInt32(UInt32)
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,20
       vzeroupper
       mov       rdi,rcx
       mov       esi,edx
       cmp       [rdi],edi
       lea       rbx,[rdi+18]
       mov       rcx,rbx
       mov       ecx,[rcx+10]
       mov       edx,[rdi+10]
       sub       ecx,edx
       cmp       ecx,5
       jl        short M01_L00
       cmp       [rdi],edi
       cmp       edx,[rbx+10]
       ja        near ptr M01_L09
       mov       r11,[rbx+8]
       movsxd    rdx,edx
       add       rdx,r11
       mov       ebp,ecx
       mov       rcx,[rbx]
       mov       r14,rcx
       mov       ecx,ebp
       mov       ebp,ecx
       jmp       short M01_L04
M01_L00:
       test      edx,edx
       jle       short M01_L01
       test      edx,edx
       je        short M01_L01
       mov       rcx,[rdi]
       mov       r11,7FFD9D4C0390
       mov       rax,7FFD9D4C0390
       cmp       [rcx],ecx
       call      qword ptr [rax]
       xor       edx,edx
       mov       [rdi+10],edx
       vxorps    xmm0,xmm0,xmm0
       vmovdqu   xmmword ptr [rbx],xmm0
       mov       [rbx+10],rdx
M01_L01:
       mov       rcx,[rdi]
       mov       rdx,[rdi+8]
       mov       edx,[rdx+0C]
       cmp       edx,5
       jle       short M01_L02
       mov       r8d,edx
       jmp       short M01_L03
M01_L02:
       mov       r8d,5
M01_L03:
       cmp       [rdi],edi
       mov       rdx,rbx
       mov       r11,7FFD9D4C0388
       mov       rax,7FFD9D4C0388
       cmp       [rcx],ecx
       call      qword ptr [rax]
       mov       rcx,rbx
       mov       r14,[rcx]
       mov       rdx,[rcx+8]
       mov       ebp,[rcx+10]
M01_L04:
       test      ebp,ebp
       jbe       short M01_L10
       test      r14,r14
       jne       short M01_L05
       mov       rcx,rdx
       mov       rdx,rcx
       mov       rcx,rdx
       jmp       short M01_L06
M01_L05:
       lea       rcx,[r14+8]
       add       rcx,rdx
M01_L06:
       mov       [rcx],sil
       cmp       esi,7F
       ja        short M01_L07
       inc       dword ptr [rdi+10]
       add       rsp,20
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M01_L07:
       xor       eax,eax
M01_L08:
       lea       edx,[rax+1]
       mov       r8d,esi
       or        r8d,80
       movsxd    rax,eax
       mov       [rcx+rax],r8b
       shr       esi,7
       cmp       esi,7F
       mov       eax,edx
       ja        short M01_L08
       movsxd    rdx,eax
       mov       [rcx+rdx],sil
       lea       ecx,[rax+1]
       test      ecx,ecx
       jl        short M01_L11
       add       [rdi+10],ecx
       add       rsp,20
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M01_L09:
       mov       ecx,1
       call      00007FFD9D5DCC10
       int       3
M01_L10:
       call      00007FFD9D5DCC1C
       int       3
M01_L11:
       mov       rcx,offset MT_System.ArgumentOutOfRangeException
       call      CORINFO_HELP_NEWSFAST
       mov       rsi,rax
       mov       ecx,269
       mov       rdx,7FFD9D708408
       call      CORINFO_HELP_STRCNS
       mov       rdx,rax
       mov       rcx,rsi
       call      System.ArgumentOutOfRangeException..ctor(System.String)
       mov       rcx,rsi
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 381
```

## .NET Framework 4.8.1 (4.8.9325.0), X64 RyuJIT VectorSize=256 (Job: net48-x64(Platform=X64, Runtime=.NET Framework 4.8))

```asm
; Benchmark.WriteVarInt.WriteAny()
       push      rdi
       push      rsi
       sub       rsp,58
       vzeroupper
       mov       rsi,rcx
       lea       rdi,[rsp+28]
       mov       ecx,0C
       xor       eax,eax
       rep stosd
       mov       rcx,rsi
       mov       rsi,rcx
       mov       rdx,[rsi+38]
       xor       ecx,ecx
       mov       [rsp+38],ecx
       lea       rax,[rsp+40]
       vxorps    xmm0,xmm0,xmm0
       vmovdqu   xmmword ptr [rax],xmm0
       mov       [rax+10],rcx
       mov       [rsp+3C],ecx
       mov       [rsp+28],rdx
       lea       rdx,[rsp+28]
       mov       rcx,194CC12CC60
       mov       rax,[rcx]
       lea       rcx,[rdx+8]
       mov       rdx,rax
       call      CORINFO_HELP_CHECKED_ASSIGN_REF
       xor       edi,edi
M00_L00:
       lea       rcx,[rsp+28]
       mov       rdx,[rsi+30]
       cmp       edi,[rdx+8]
       jae       short M00_L01
       movsxd    rax,edi
       mov       edx,[rdx+rax*4+10]
       call      Bshox.BshoxWriter.WriteVarInt32(UInt32)
       inc       edi
       cmp       edi,3E8
       jl        short M00_L00
       mov       rax,[rsi+38]
       lea       rsi,[rax+18]
       lea       rdi,[rax+8]
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,[rsp+38]
       add       rsp,58
       pop       rsi
       pop       rdi
       ret
M00_L01:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 174
```
```asm
; Bshox.BshoxWriter.WriteVarInt32(UInt32)
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,20
       vzeroupper
       mov       rdi,rcx
       mov       esi,edx
       cmp       [rdi],edi
       lea       rbx,[rdi+18]
       mov       rcx,rbx
       mov       ecx,[rcx+10]
       mov       edx,[rdi+10]
       sub       ecx,edx
       cmp       ecx,5
       jl        short M01_L00
       cmp       [rdi],edi
       cmp       edx,[rbx+10]
       ja        near ptr M01_L09
       mov       r11,[rbx+8]
       movsxd    rdx,edx
       add       rdx,r11
       mov       ebp,ecx
       mov       rcx,[rbx]
       mov       r14,rcx
       mov       ecx,ebp
       mov       ebp,ecx
       jmp       short M01_L04
M01_L00:
       test      edx,edx
       jle       short M01_L01
       test      edx,edx
       je        short M01_L01
       mov       rcx,[rdi]
       mov       r11,7FFD9D4C0390
       mov       rax,7FFD9D4C0390
       cmp       [rcx],ecx
       call      qword ptr [rax]
       xor       edx,edx
       mov       [rdi+10],edx
       vxorps    xmm0,xmm0,xmm0
       vmovdqu   xmmword ptr [rbx],xmm0
       mov       [rbx+10],rdx
M01_L01:
       mov       rcx,[rdi]
       mov       rdx,[rdi+8]
       mov       edx,[rdx+0C]
       cmp       edx,5
       jle       short M01_L02
       mov       r8d,edx
       jmp       short M01_L03
M01_L02:
       mov       r8d,5
M01_L03:
       cmp       [rdi],edi
       mov       rdx,rbx
       mov       r11,7FFD9D4C0388
       mov       rax,7FFD9D4C0388
       cmp       [rcx],ecx
       call      qword ptr [rax]
       mov       rcx,rbx
       mov       r14,[rcx]
       mov       rdx,[rcx+8]
       mov       ebp,[rcx+10]
M01_L04:
       test      ebp,ebp
       jbe       short M01_L10
       test      r14,r14
       jne       short M01_L05
       mov       rcx,rdx
       mov       rdx,rcx
       mov       rcx,rdx
       jmp       short M01_L06
M01_L05:
       lea       rcx,[r14+8]
       add       rcx,rdx
M01_L06:
       mov       [rcx],sil
       cmp       esi,7F
       ja        short M01_L07
       inc       dword ptr [rdi+10]
       add       rsp,20
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M01_L07:
       xor       eax,eax
M01_L08:
       lea       edx,[rax+1]
       mov       r8d,esi
       or        r8d,80
       movsxd    rax,eax
       mov       [rcx+rax],r8b
       shr       esi,7
       cmp       esi,7F
       mov       eax,edx
       ja        short M01_L08
       movsxd    rdx,eax
       mov       [rcx+rdx],sil
       lea       ecx,[rax+1]
       test      ecx,ecx
       jl        short M01_L11
       add       [rdi+10],ecx
       add       rsp,20
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M01_L09:
       mov       ecx,1
       call      00007FFD9D5DCC10
       int       3
M01_L10:
       call      00007FFD9D5DCC1C
       int       3
M01_L11:
       mov       rcx,offset MT_System.ArgumentOutOfRangeException
       call      CORINFO_HELP_NEWSFAST
       mov       rsi,rax
       mov       ecx,269
       mov       rdx,7FFD9D708408
       call      CORINFO_HELP_STRCNS
       mov       rdx,rax
       mov       rcx,rsi
       call      System.ArgumentOutOfRangeException..ctor(System.String)
       mov       rcx,rsi
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 381
```