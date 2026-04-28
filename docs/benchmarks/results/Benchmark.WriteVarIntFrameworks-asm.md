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
       mov       rax,2B88AC04F90
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
       call      qword ptr [7FFD80FED488]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
M00_L09:
       call      qword ptr [7FFD80F7C4E0]
       int       3
M00_L10:
       mov       rcx,[rsp+40]
       mov       edx,[rsp+5C]
       mov       r11,7FFD80CA04C0
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
       mov       r11,7FFD80CA04B8
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
       mov       rax,2CC3D004F90
       mov       rax,[rax]
       mov       [rsp+48],rax
       xor       ebp,ebp
M00_L00:
       mov       rax,[rbx+8]
       cmp       ebp,[rax+8]
       jae       near ptr M00_L16
       mov       ecx,ebp
       mov       r14d,[rax+rcx*4+10]
       vxorps    xmm0,xmm0,xmm0
       vmovdqu   xmmword ptr [rsp+30],xmm0
       cmp       dword ptr [rsp+58],5
       jl        short M00_L02
       mov       r12,[rsp+50]
       xor       r15d,r15d
       cmp       r14d,7F
       ja        near ptr M00_L08
M00_L01:
       movsxd    rax,r15d
       mov       [r12+rax],r14b
       lea       r13d,[r15+1]
       test      r13d,r13d
       jl        near ptr M00_L14
       movsxd    rax,r13d
       add       rax,[rsp+50]
       mov       [rsp+50],rax
       mov       eax,[rsp+58]
       sub       eax,r13d
       mov       [rsp+58],eax
       add       r13d,[rsp+5C]
       mov       [rsp+5C],r13d
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
M00_L02:
       cmp       dword ptr [rsp+5C],0
       jg        near ptr M00_L15
M00_L03:
       mov       rcx,[rsp+40]
       mov       rax,[rsp+48]
       mov       r8d,[rax+0C]
       cmp       r8d,5
       jle       near ptr M00_L09
M00_L04:
       mov       rax,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rcx],rax
       jne       near ptr M00_L11
       mov       edx,[rcx+14]
       mov       r15d,edx
       cmp       r8d,r15d
       jg        near ptr M00_L12
       mov       rax,[rcx+8]
       mov       r13d,[rcx+10]
       xor       r12d,r12d
       xor       ecx,ecx
       test      rax,rax
       je        short M00_L06
       mov       rcx,[rax]
       test      dword ptr [rcx],80000000
       je        near ptr M00_L10
       lea       r12,[rax+10]
       mov       ecx,[rax+8]
M00_L05:
       and       r13d,7FFFFFFF
       mov       eax,r13d
       mov       edx,r15d
       add       rdx,rax
       mov       ecx,ecx
       cmp       rdx,rcx
       ja        near ptr M00_L13
       add       r12,rax
       mov       ecx,r15d
M00_L06:
       mov       rax,r12
       mov       [rsp+30],rax
       mov       [rsp+38],ecx
M00_L07:
       cmp       dword ptr [rsp+38],0
       jbe       near ptr M00_L16
       mov       rax,[rsp+30]
       mov       [rsp+50],rax
       mov       eax,[rsp+38]
       mov       [rsp+58],eax
       mov       r12,[rsp+50]
       xor       r15d,r15d
       cmp       r14d,7F
       jbe       near ptr M00_L01
       xchg      ax,ax
M00_L08:
       lea       eax,[r15+1]
       mov       ecx,r14d
       or        ecx,0FFFFFF80
       movsxd    rdx,r15d
       mov       [r12+rdx],cl
       shr       r14d,7
       cmp       r14d,7F
       mov       r15d,eax
       ja        short M00_L08
       jmp       near ptr M00_L01
M00_L09:
       mov       r8d,5
       jmp       near ptr M00_L04
M00_L10:
       lea       rdx,[rsp+20]
       mov       rcx,rax
       mov       rax,[rax]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       r12,[rsp+20]
       mov       ecx,[rsp+28]
       jmp       near ptr M00_L05
M00_L11:
       lea       rdx,[rsp+30]
       mov       r11,7FFD80CA04B8
       call      qword ptr [r11]
       jmp       near ptr M00_L07
M00_L12:
       mov       edx,r15d
       mov       ecx,r8d
       call      qword ptr [7FFD80FED488]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
M00_L13:
       call      qword ptr [7FFD80F7C4E0]
       int       3
M00_L14:
       mov       ecx,225
       mov       rdx,7FFD811E8198
       call      CORINFO_HELP_STRCNS
       mov       rdx,rax
       mov       ecx,r13d
       call      qword ptr [7FFD811DF4E0]
       int       3
M00_L15:
       mov       rcx,[rsp+40]
       mov       edx,[rsp+5C]
       mov       r11,7FFD80CA04C0
       call      qword ptr [r11]
       xor       edx,edx
       mov       [rsp+50],rdx
       mov       [rsp+58],edx
       mov       [rsp+5C],edx
       jmp       near ptr M00_L03
M00_L16:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 657
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
       mov       rax,1D15EC04F90
       mov       rax,[rax]
       mov       [rsp+50],rax
       xor       ebp,ebp
M00_L00:
       mov       rax,[rbx+30]
       cmp       ebp,[rax+8]
       jae       near ptr M00_L19
       mov       ecx,ebp
       mov       r14d,[rax+rcx*4+10]
       vxorps    xmm0,xmm0,xmm0
       vmovdqu   xmmword ptr [rsp+38],xmm0
       cmp       dword ptr [rsp+60],5
       jl        near ptr M00_L07
M00_L01:
       mov       rax,[rsp+58]
       xor       ecx,ecx
       cmp       r14d,7F
       jbe       near ptr M00_L05
M00_L02:
       lea       edx,[rcx+1]
       mov       r8d,r14d
       or        r8d,0FFFFFF80
       movsxd    rcx,ecx
       mov       [rax+rcx],r8b
       shr       r14d,7
       cmp       r14d,7F
       ja        short M00_L04
M00_L03:
       movsxd    rcx,edx
       mov       [rax+rcx],r14b
       lea       r15d,[rdx+1]
       test      r15d,r15d
       jl        near ptr M00_L16
       movsxd    rax,r15d
       add       rax,[rsp+58]
       mov       [rsp+58],rax
       mov       eax,[rsp+60]
       sub       eax,r15d
       mov       [rsp+60],eax
       add       r15d,[rsp+64]
       mov       [rsp+64],r15d
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
       mov       ecx,edx
       jmp       near ptr M00_L02
M00_L05:
       mov       edx,ecx
       jmp       short M00_L03
M00_L06:
       mov       rcx,[rsp+48]
       mov       rdx,[rsp+50]
       mov       r8d,[rdx+0C]
       cmp       r8d,5
       jle       short M00_L08
       jmp       short M00_L09
M00_L07:
       cmp       dword ptr [rsp+64],0
       jg        near ptr M00_L17
       jmp       short M00_L06
M00_L08:
       mov       r8d,5
M00_L09:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rcx],rdx
       jne       near ptr M00_L18
       mov       edx,[rcx+14]
       mov       esi,edx
       cmp       r8d,esi
       jg        near ptr M00_L14
       mov       rdx,[rcx+8]
       mov       edi,[rcx+10]
       xor       r15d,r15d
       xor       r13d,r13d
       mov       rcx,rdx
       test      rcx,rcx
       je        short M00_L12
       mov       rdx,[rcx]
       test      dword ptr [rdx],80000000
       je        short M00_L10
       lea       r15,[rcx+10]
       mov       r13d,[rcx+8]
       jmp       short M00_L11
M00_L10:
       lea       rdx,[rsp+28]
       mov       rax,[rcx]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       r15,[rsp+28]
       mov       r13d,[rsp+30]
M00_L11:
       and       edi,7FFFFFFF
       mov       ecx,edi
       mov       edx,esi
       add       rdx,rcx
       mov       r11d,r13d
       cmp       rdx,r11
       ja        short M00_L15
       add       r15,rcx
       mov       r13d,esi
M00_L12:
       mov       rcx,r15
       mov       [rsp+38],rcx
       mov       [rsp+40],r13d
M00_L13:
       cmp       dword ptr [rsp+40],0
       jbe       near ptr M00_L19
       mov       rcx,[rsp+38]
       mov       [rsp+58],rcx
       mov       ecx,[rsp+40]
       mov       [rsp+60],ecx
       jmp       near ptr M00_L01
M00_L14:
       mov       edx,esi
       mov       ecx,r8d
       call      qword ptr [7FFD8240D488]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
M00_L15:
       call      qword ptr [7FFD8239C4E0]
       int       3
M00_L16:
       mov       ecx,225
       mov       rdx,7FFD826085C8
       call      CORINFO_HELP_STRCNS
       mov       rdx,rax
       mov       ecx,r15d
       call      qword ptr [7FFD825FF4E0]
       int       3
M00_L17:
       mov       rcx,[rsp+48]
       mov       edx,[rsp+64]
       mov       r11,7FFD820C04C0
       call      qword ptr [r11]
       xor       edx,edx
       mov       [rsp+58],rdx
       mov       [rsp+60],edx
       mov       [rsp+64],edx
       jmp       near ptr M00_L06
M00_L18:
       lea       rdx,[rsp+38]
       mov       r11,7FFD820C04B8
       call      qword ptr [r11]
       jmp       near ptr M00_L13
M00_L19:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 631
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
       mov       rax,17E7BC01320
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
       call      qword ptr [7FFD82423400]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
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
       mov       r11,7FFD820D04E0
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
       call      qword ptr [7FFD823773A8]
       int       3
M00_L12:
       lea       rdx,[rsp+48]
       mov       rcx,rbp
       mov       r11,7FFD820D04D8
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
       call      qword ptr [7FFD8252EEF8]
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
       mov       rax,1C7A4C01320
       mov       r12,[rax]
       xor       esi,esi
M00_L00:
       mov       rax,[rbx+8]
       cmp       esi,[rax+8]
       jae       near ptr M00_L16
       mov       edi,[rax+rsi*4+10]
       cmp       r15d,5
       jl        short M00_L04
M00_L01:
       xor       eax,eax
       cmp       edi,7F
       jbe       short M00_L03
M00_L02:
       lea       ecx,[rax+1]
       mov       edx,edi
       or        edx,0FFFFFF80
       cdqe
       mov       [r14+rax],dl
       shr       edi,7
       cmp       edi,7F
       mov       eax,ecx
       ja        short M00_L02
M00_L03:
       movsxd    rcx,eax
       mov       [r14+rcx],dil
       lea       edi,[rax+1]
       test      edi,edi
       jl        near ptr M00_L15
       movsxd    rax,edi
       add       r14,rax
       sub       r15d,edi
       add       r13d,edi
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
       jg        near ptr M00_L10
M00_L05:
       mov       r8d,[r12+0C]
       cmp       r8d,5
       jle       near ptr M00_L11
M00_L06:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rbp],rdx
       jne       near ptr M00_L14
       lea       rdx,[rsp+38]
       mov       rcx,rbp
       call      qword ptr [7FFD82413400]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       ecx,ecx
       xor       edx,edx
       mov       rax,[rsp+38]
       test      rax,rax
       je        short M00_L08
       mov       rcx,[rax]
       test      dword ptr [rcx],80000000
       je        short M00_L12
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
       ja        short M00_L13
       add       rcx,r11
       mov       edx,eax
M00_L08:
       mov       [rsp+48],rcx
       mov       [rsp+50],edx
M00_L09:
       cmp       dword ptr [rsp+50],0
       jbe       near ptr M00_L16
       mov       r14,[rsp+48]
       mov       r15d,[rsp+50]
       jmp       near ptr M00_L01
M00_L10:
       mov       rcx,rbp
       mov       edx,r13d
       mov       r11,7FFD820C04E0
       call      qword ptr [r11]
       xor       r13d,r13d
       jmp       near ptr M00_L05
M00_L11:
       mov       r8d,5
       jmp       near ptr M00_L06
M00_L12:
       lea       rdx,[rsp+28]
       mov       rcx,rax
       mov       rax,[rax]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rcx,[rsp+28]
       mov       edx,[rsp+30]
       jmp       near ptr M00_L07
M00_L13:
       call      qword ptr [7FFD823673A8]
       int       3
M00_L14:
       lea       rdx,[rsp+48]
       mov       rcx,rbp
       mov       r11,7FFD820C04D8
       call      qword ptr [r11]
       jmp       short M00_L09
M00_L15:
       mov       ecx,225
       mov       rdx,7FFD825A0FC8
       call      CORINFO_HELP_STRCNS
       mov       rdx,rax
       mov       ecx,edi
       call      qword ptr [7FFD8251F0D8]
       int       3
M00_L16:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 499
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
       call      qword ptr [7FFD8251EF28]
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
       mov       rax,2437A401320
       mov       r12,[rax]
       xor       esi,esi
M00_L00:
       mov       rax,[rbx+30]
       cmp       esi,[rax+8]
       jae       near ptr M00_L19
       mov       edi,[rax+rsi*4+10]
       cmp       r15d,5
       jl        short M00_L07
M00_L01:
       xor       eax,eax
       cmp       edi,7F
       jbe       short M00_L05
M00_L02:
       lea       ecx,[rax+1]
       mov       edx,edi
       or        edx,0FFFFFF80
       cdqe
       mov       [r14+rax],dl
       shr       edi,7
       cmp       edi,7F
       ja        short M00_L04
M00_L03:
       movsxd    rax,ecx
       mov       [r14+rax],dil
       lea       edi,[rcx+1]
       test      edi,edi
       jge       short M00_L06
       jmp       near ptr M00_L18
M00_L04:
       mov       eax,ecx
       jmp       short M00_L02
M00_L05:
       mov       ecx,eax
       jmp       short M00_L03
M00_L06:
       movsxd    rax,edi
       add       r14,rax
       sub       r15d,edi
       add       r13d,edi
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
M00_L07:
       test      r13d,r13d
       jg        near ptr M00_L15
M00_L08:
       mov       r8d,[r12+0C]
       cmp       r8d,5
       jle       short M00_L09
       jmp       short M00_L10
M00_L09:
       mov       r8d,5
M00_L10:
       mov       rdx,offset MT_Bshox.TestUtils.FixedBufferWriter
       cmp       [rbp],rdx
       jne       near ptr M00_L17
       lea       rdx,[rsp+38]
       mov       rcx,rbp
       call      qword ptr [7FFD82413400]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
       xor       r14d,r14d
       xor       r15d,r15d
       mov       rcx,[rsp+38]
       test      rcx,rcx
       je        short M00_L13
       mov       rdx,[rcx]
       test      dword ptr [rdx],80000000
       jne       short M00_L11
       lea       rdx,[rsp+28]
       mov       rax,[rcx]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       r15d,[rsp+30]
       mov       r14,[rsp+28]
       jmp       short M00_L12
M00_L11:
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L12:
       mov       ecx,[rsp+40]
       and       ecx,7FFFFFFF
       mov       edx,[rsp+44]
       mov       r11d,edx
       add       r11,rcx
       mov       eax,r15d
       cmp       r11,rax
       ja        short M00_L16
       add       r14,rcx
       mov       r15d,edx
M00_L13:
       mov       rcx,r14
       mov       [rsp+48],rcx
       mov       [rsp+50],r15d
M00_L14:
       cmp       dword ptr [rsp+50],0
       jbe       short M00_L19
       mov       r14,[rsp+48]
       mov       r15d,[rsp+50]
       jmp       near ptr M00_L01
M00_L15:
       mov       rcx,rbp
       mov       edx,r13d
       mov       r11,7FFD820C04E0
       call      qword ptr [r11]
       xor       r13d,r13d
       jmp       near ptr M00_L08
M00_L16:
       call      qword ptr [7FFD823673A8]
       int       3
M00_L17:
       lea       rdx,[rsp+48]
       mov       rcx,rbp
       mov       r11,7FFD820C04D8
       call      qword ptr [r11]
       jmp       short M00_L14
M00_L18:
       mov       ecx,225
       mov       rdx,7FFD825A0FC8
       call      CORINFO_HELP_STRCNS
       mov       rdx,rax
       mov       ecx,edi
       call      qword ptr [7FFD8251F0D8]
       int       3
M00_L19:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 497
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
       call      qword ptr [7FFD8251EF28]
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
       mov       rcx,1E39B401318
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
       call      qword ptr [7FFD706DFFA8]
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
       mov       rcx,25723801318
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
       call      qword ptr [7FFD705F7440]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
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
       mov       r11,7FFD702E0500
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
       mov       r11,7FFD702E04F8
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
       call      qword ptr [7FFD70567990]
       int       3
M00_L15:
       mov       ecx,225
       mov       rdx,7FFD70754DE0
       call      qword ptr [7FFD7039EE38]
       mov       rdx,rax
       mov       ecx,r12d
       call      qword ptr [7FFD70774180]
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
       call      qword ptr [7FFD706FFFC0]
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
       mov       rcx,273D0401318
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
       call      qword ptr [7FFD705F7440]; Bshox.TestUtils.FixedBufferWriter.GetMemory(Int32)
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
       call      qword ptr [7FFD70567990]
       int       3
M00_L14:
       mov       rcx,rdi
       mov       edx,r14d
       mov       r11,7FFD702E0500
       call      qword ptr [r11]
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L15:
       lea       rdx,[rsp+48]
       mov       rcx,rdi
       mov       r11,7FFD702E04F8
       call      qword ptr [r11]
       jmp       near ptr M00_L06
M00_L16:
       mov       ecx,225
       mov       rdx,7FFD70754DE0
       call      qword ptr [7FFD7039EE38]
       mov       rdx,rax
       mov       ecx,r12d
       call      qword ptr [7FFD70774180]
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
       call      qword ptr [7FFD706FFFC0]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 48
```

## .NET Framework 4.8.1 (4.8.9325.0), X64 RyuJIT VectorSize=256 (Job: net48-x64(Platform=X64, Runtime=.NET Framework 4.8))

```asm
; Benchmark.WriteVarInt.WriteByte()
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,50
       vzeroupper
       mov       rsi,rcx
       lea       rdi,[rsp+20]
       mov       ecx,0C
       xor       eax,eax
       rep stosd
       mov       rcx,rsi
       mov       rsi,rcx
       mov       rdx,[rsi+38]
       xor       ecx,ecx
       mov       [rsp+30],ecx
       lea       rax,[rsp+38]
       vxorps    xmm0,xmm0,xmm0
       vmovdqu   xmmword ptr [rax],xmm0
       mov       [rax+10],rcx
       mov       [rsp+34],ecx
       mov       [rsp+20],rdx
       lea       rdx,[rsp+20]
       mov       rcx,1CE1000CC60
       mov       rax,[rcx]
       lea       rcx,[rdx+8]
       mov       rdx,rax
       call      CORINFO_HELP_CHECKED_ASSIGN_REF
       xor       edi,edi
M00_L00:
       mov       rcx,[rsi+8]
       cmp       edi,[rcx+8]
       jae       near ptr M00_L10
       movsxd    rdx,edi
       movzx     ebx,byte ptr [rcx+rdx*4+10]
       mov       ecx,[rsp+48]
       sub       ecx,[rsp+30]
       test      ecx,ecx
       jle       short M00_L01
       mov       ecx,[rsp+30]
       cmp       ecx,[rsp+48]
       ja        near ptr M00_L08
       mov       rdx,[rsp+40]
       movsxd    r11,ecx
       add       rdx,r11
       mov       r11d,[rsp+48]
       sub       r11d,ecx
       mov       ebp,r11d
       mov       rcx,[rsp+38]
       mov       r14,rcx
       mov       r11d,ebp
       mov       rcx,r14
       mov       r14,rcx
       mov       ebp,r11d
       jmp       near ptr M00_L05
M00_L01:
       cmp       dword ptr [rsp+30],0
       jle       short M00_L02
       cmp       dword ptr [rsp+30],0
       je        short M00_L02
       mov       rcx,[rsp+20]
       mov       edx,[rsp+30]
       mov       r11,7FFD9D4B0390
       mov       rax,7FFD9D4B0390
       cmp       [rcx],ecx
       call      qword ptr [rax]
       xor       edx,edx
       mov       [rsp+30],edx
       lea       rcx,[rsp+38]
       vxorps    xmm0,xmm0,xmm0
       vmovdqu   xmmword ptr [rcx],xmm0
       mov       [rcx+10],rdx
M00_L02:
       mov       rcx,[rsp+20]
       mov       rdx,[rsp+28]
       mov       edx,[rdx+0C]
       cmp       edx,1
       jle       short M00_L03
       mov       r8d,edx
       jmp       short M00_L04
M00_L03:
       mov       r8d,1
M00_L04:
       lea       rdx,[rsp+38]
       mov       r11,7FFD9D4B0388
       mov       rax,7FFD9D4B0388
       cmp       [rcx],ecx
       call      qword ptr [rax]
       lea       rcx,[rsp+38]
       mov       r14,[rcx]
       mov       rdx,[rcx+8]
       mov       ebp,[rcx+10]
M00_L05:
       test      ebp,ebp
       jbe       short M00_L09
       test      r14,r14
       jne       short M00_L06
       mov       rcx,rdx
       mov       rdx,rcx
       mov       rcx,rdx
       jmp       short M00_L07
M00_L06:
       lea       rcx,[r14+8]
       add       rcx,rdx
M00_L07:
       mov       [rcx],bl
       lea       rcx,[rsp+20]
       mov       edx,1
       call      Bshox.BshoxWriter.Advance(Int32)
       inc       edi
       cmp       edi,3E8
       jl        near ptr M00_L00
       mov       rax,[rsi+38]
       lea       rsi,[rax+18]
       lea       rdi,[rax+8]
       call      CORINFO_HELP_ASSIGN_BYREF
       movsq
       mov       eax,[rsp+30]
       add       rsp,50
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M00_L08:
       mov       ecx,1
       call      00007FFD9D5CCBE0
       int       3
M00_L09:
       call      00007FFD9D5CCBEC
       int       3
M00_L10:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 460
```
```asm
; Bshox.BshoxWriter.Advance(Int32)
       push      rsi
       sub       rsp,20
       test      edx,edx
       jl        short M01_L00
       add       [rcx+10],edx
       add       rsp,20
       pop       rsi
       ret
M01_L00:
       mov       rcx,offset MT_System.ArgumentOutOfRangeException
       call      CORINFO_HELP_NEWSFAST
       mov       rsi,rax
       mov       ecx,269
       mov       rdx,7FFD9D6F8408
       call      CORINFO_HELP_STRCNS
       mov       rdx,rax
       mov       rcx,rsi
       call      System.ArgumentOutOfRangeException..ctor(System.String)
       mov       rcx,rsi
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 76
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
       mov       rcx,1DEED3ECC60
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
       ja        near ptr M01_L11
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
       jbe       short M01_L12
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
       xor       eax,eax
       cmp       esi,7F
       jbe       short M01_L10
M01_L07:
       lea       edx,[rax+1]
       mov       r8d,esi
       or        r8d,0FFFFFF80
       movsxd    rax,eax
       mov       [rcx+rax],r8b
       shr       esi,7
       cmp       esi,7F
       ja        short M01_L09
M01_L08:
       movsxd    rax,edx
       mov       [rcx+rax],sil
       lea       ecx,[rdx+1]
       test      ecx,ecx
       jl        short M01_L13
       add       [rdi+10],ecx
       add       rsp,20
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M01_L09:
       mov       eax,edx
       jmp       short M01_L07
M01_L10:
       mov       edx,eax
       jmp       short M01_L08
M01_L11:
       mov       ecx,1
       call      00007FFD9D5DCBF0
       int       3
M01_L12:
       call      00007FFD9D5DCBFC
       int       3
M01_L13:
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
; Total bytes of code 367
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
       mov       rcx,1C5BC3CCC60
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
       ja        near ptr M01_L11
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
       mov       r11,7FFD9D4E0390
       mov       rax,7FFD9D4E0390
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
       mov       r11,7FFD9D4E0388
       mov       rax,7FFD9D4E0388
       cmp       [rcx],ecx
       call      qword ptr [rax]
       mov       rcx,rbx
       mov       r14,[rcx]
       mov       rdx,[rcx+8]
       mov       ebp,[rcx+10]
M01_L04:
       test      ebp,ebp
       jbe       short M01_L12
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
       xor       eax,eax
       cmp       esi,7F
       jbe       short M01_L10
M01_L07:
       lea       edx,[rax+1]
       mov       r8d,esi
       or        r8d,0FFFFFF80
       movsxd    rax,eax
       mov       [rcx+rax],r8b
       shr       esi,7
       cmp       esi,7F
       ja        short M01_L09
M01_L08:
       movsxd    rax,edx
       mov       [rcx+rax],sil
       lea       ecx,[rdx+1]
       test      ecx,ecx
       jl        short M01_L13
       add       [rdi+10],ecx
       add       rsp,20
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M01_L09:
       mov       eax,edx
       jmp       short M01_L07
M01_L10:
       mov       edx,eax
       jmp       short M01_L08
M01_L11:
       mov       ecx,1
       call      00007FFD9D5FCBF0
       int       3
M01_L12:
       call      00007FFD9D5FCBFC
       int       3
M01_L13:
       mov       rcx,offset MT_System.ArgumentOutOfRangeException
       call      CORINFO_HELP_NEWSFAST
       mov       rsi,rax
       mov       ecx,269
       mov       rdx,7FFD9D728408
       call      CORINFO_HELP_STRCNS
       mov       rdx,rax
       mov       rcx,rsi
       call      System.ArgumentOutOfRangeException..ctor(System.String)
       mov       rcx,rsi
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 367
```