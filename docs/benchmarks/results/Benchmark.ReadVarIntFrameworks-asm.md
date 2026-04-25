## .NET 8.0.26 (8.0.26, 8.0.2626.16921), X64 RyuJIT x86-64-v3 (Job: net8.0-x64(Platform=X64, Runtime=.NET 8.0))

```asm
; Benchmark.ReadVarInt.ReadByte()
;         var r = new BshoxReader(buffer1);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         for (int i = 0; i < Count; i++)
;              ^^^^^^^^^
;             _ = r.ReadByte();
;             ^^^^^^^^^^^^^^^^^
;         return r.Consumed;
;         ^^^^^^^^^^^^^^^^^^
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,88
       vxorps    xmm4,xmm4,xmm4
       mov       rax,0FFFFFFFFFFFFFFA0
M00_L00:
       vmovdqa   xmmword ptr [rsp+rax+80],xmm4
       vmovdqa   xmmword ptr [rsp+rax+90],xmm4
       vmovdqa   xmmword ptr [rsp+rax+0A0],xmm4
       add       rax,30
       jne       short M00_L00
       mov       [rsp+80],rax
       mov       rax,[rcx+38]
       mov       ebx,[rcx+40]
       mov       esi,[rcx+44]
       mov       rdx,1A20A804DB8
       mov       rdx,[rdx]
       mov       [rsp+30],rdx
       xor       edx,edx
       mov       [rsp+38],rdx
       mov       byte ptr [rsp+4C],0
       movsxd    rdx,esi
       mov       [rsp+40],rdx
       xor       edi,edi
       xor       ebp,ebp
       mov       rcx,rax
       test      rcx,rcx
       je        short M00_L02
       mov       rax,[rcx]
       test      dword ptr [rax],80000000
       je        near ptr M00_L09
       lea       rdi,[rcx+10]
       mov       ebp,[rcx+8]
M00_L01:
       and       ebx,7FFFFFFF
       mov       eax,ebx
       mov       edx,esi
       add       rdx,rax
       mov       ecx,ebp
       cmp       rdx,rcx
       ja        near ptr M00_L08
       add       rdi,rax
       mov       ebp,esi
M00_L02:
       mov       rax,rdi
       mov       [rsp+50],rax
       mov       [rsp+58],ebp
       xor       eax,eax
       test      esi,esi
       setne     al
       mov       [rsp+4D],al
       xor       ebx,ebx
M00_L03:
       cmp       byte ptr [rsp+4D],0
       je        short M00_L07
       mov       eax,[rsp+58]
       test      eax,eax
       je        near ptr M00_L11
       mov       rdx,[rsp+50]
       cmp       [rdx],dl
       test      eax,eax
       je        short M00_L08
       inc       rdx
       dec       eax
       mov       [rsp+50],rdx
       mov       [rsp+58],eax
       mov       rax,[rsp+38]
       inc       rax
       mov       [rsp+38],rax
       cmp       dword ptr [rsp+58],0
       je        short M00_L05
M00_L04:
       inc       ebx
       cmp       ebx,3E8
       jl        short M00_L03
       jmp       short M00_L06
M00_L05:
       cmp       byte ptr [rsp+4C],0
       jne       short M00_L10
       mov       byte ptr [rsp+4D],0
       jmp       short M00_L04
M00_L06:
       mov       rax,[rsp+38]
       add       rsp,88
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M00_L07:
       call      qword ptr [7FF82E5266A0]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
M00_L08:
       call      qword ptr [7FF82E29C4E0]
       int       3
M00_L09:
       lea       rdx,[rsp+20]
       mov       rax,[rcx]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rdi,[rsp+20]
       mov       ebp,[rsp+28]
       jmp       near ptr M00_L01
M00_L10:
       lea       rcx,[rsp+30]
       call      qword ptr [7FF82E526610]
       jmp       short M00_L04
M00_L11:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 378
```

## .NET 8.0.26 (8.0.26, 8.0.2626.16921), X64 RyuJIT x86-64-v3 (Job: net8.0-x64(Platform=X64, Runtime=.NET 8.0))

```asm
; Benchmark.ReadVarInt.Read1()
;         var r = new BshoxReader(buffer1);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         for (int i = 0; i < Count; i++)
;              ^^^^^^^^^
;             _ = r.ReadVarInt32();
;             ^^^^^^^^^^^^^^^^^^^^^
;         return r.Consumed;
;         ^^^^^^^^^^^^^^^^^^
       push      r15
       push      r14
       push      r13
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,90
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       mov       rax,0FFFFFFFFFFFFFFA0
M00_L00:
       vmovdqa   xmmword ptr [rsp+rax+90],xmm4
       vmovdqa   xmmword ptr [rsp+rax+0A0],xmm4
       vmovdqa   xmmword ptr [rsp+rax+0B0],xmm4
       add       rax,30
       jne       short M00_L00
       mov       rax,[rcx+38]
       mov       ebx,[rcx+40]
       mov       esi,[rcx+44]
       mov       rdx,129EA004DB8
       mov       rdx,[rdx]
       mov       [rsp+38],rdx
       xor       edx,edx
       mov       [rsp+40],rdx
       mov       byte ptr [rsp+54],0
       movsxd    rdx,esi
       mov       [rsp+48],rdx
       xor       edi,edi
       xor       ebp,ebp
       mov       rcx,rax
       test      rcx,rcx
       je        short M00_L02
       mov       rax,[rcx]
       test      dword ptr [rax],80000000
       je        near ptr M00_L10
       lea       rdi,[rcx+10]
       mov       ebp,[rcx+8]
M00_L01:
       and       ebx,7FFFFFFF
       mov       eax,ebx
       mov       edx,esi
       add       rdx,rax
       mov       ecx,ebp
       cmp       rdx,rcx
       ja        near ptr M00_L08
       add       rdi,rax
       mov       ebp,esi
M00_L02:
       mov       rax,rdi
       mov       [rsp+58],rax
       mov       [rsp+60],ebp
       xor       eax,eax
       test      esi,esi
       setne     al
       mov       [rsp+55],al
       xor       ebx,ebx
M00_L03:
       xor       ebp,ebp
       xor       r14d,r14d
M00_L04:
       cmp       byte ptr [rsp+55],0
       je        near ptr M00_L07
       mov       eax,[rsp+60]
       test      eax,eax
       je        near ptr M00_L13
       mov       rdx,[rsp+58]
       movzx     r15d,byte ptr [rdx]
       test      eax,eax
       je        short M00_L08
       inc       rdx
       dec       eax
       mov       [rsp+58],rdx
       mov       [rsp+60],eax
       mov       rax,[rsp+40]
       inc       rax
       mov       [rsp+40],rax
       cmp       dword ptr [rsp+60],0
       je        near ptr M00_L11
M00_L05:
       movzx     r13d,r15b
M00_L06:
       mov       eax,r13d
       and       r13d,7F
       shlx      edx,r13d,r14d
       or        ebp,edx
       add       r14d,7
       cmp       r14d,23
       jg        short M00_L09
       test      al,80
       jne       short M00_L04
       inc       ebx
       cmp       ebx,3E8
       jl        short M00_L03
       mov       rax,[rsp+40]
       add       rsp,90
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r13
       pop       r14
       pop       r15
       ret
M00_L07:
       call      qword ptr [7FF82E5466A0]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
M00_L08:
       call      qword ptr [7FF82E2BC4E0]
       int       3
M00_L09:
       call      qword ptr [7FF82E5468C8]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
M00_L10:
       lea       rdx,[rsp+28]
       mov       rax,[rcx]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rdi,[rsp+28]
       mov       ebp,[rsp+30]
       jmp       near ptr M00_L01
M00_L11:
       cmp       byte ptr [rsp+54],0
       je        short M00_L12
       lea       rcx,[rsp+38]
       call      qword ptr [7FF82E546610]
       movzx     r13d,r15b
       jmp       near ptr M00_L06
M00_L12:
       mov       byte ptr [rsp+55],0
       jmp       near ptr M00_L05
M00_L13:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 458
```

## .NET 8.0.26 (8.0.26, 8.0.2626.16921), X64 RyuJIT x86-64-v3 (Job: net8.0-x64(Platform=X64, Runtime=.NET 8.0))

```asm
; Benchmark.ReadVarInt.ReadAny()
;         var r = new BshoxReader(bufferX);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         for (int i = 0; i < Count; i++)
;              ^^^^^^^^^
;             _ = r.ReadVarInt32();
;             ^^^^^^^^^^^^^^^^^^^^^
;         return r.Consumed;
;         ^^^^^^^^^^^^^^^^^^
       push      r15
       push      r14
       push      r13
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,90
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       mov       rax,0FFFFFFFFFFFFFFA0
M00_L00:
       vmovdqa   xmmword ptr [rsp+rax+90],xmm4
       vmovdqa   xmmword ptr [rsp+rax+0A0],xmm4
       vmovdqa   xmmword ptr [rsp+rax+0B0],xmm4
       add       rax,30
       jne       short M00_L00
       mov       rax,[rcx+88]
       mov       ebx,[rcx+90]
       mov       esi,[rcx+94]
       mov       rdx,2B89A404DB8
       mov       rdx,[rdx]
       mov       [rsp+38],rdx
       xor       edx,edx
       mov       [rsp+40],rdx
       mov       byte ptr [rsp+54],0
       movsxd    rdx,esi
       mov       [rsp+48],rdx
       xor       edi,edi
       xor       ebp,ebp
       mov       rcx,rax
       test      rcx,rcx
       je        short M00_L02
       mov       rax,[rcx]
       test      dword ptr [rax],80000000
       je        near ptr M00_L10
       lea       rdi,[rcx+10]
       mov       ebp,[rcx+8]
M00_L01:
       and       ebx,7FFFFFFF
       mov       eax,ebx
       mov       edx,esi
       add       rdx,rax
       mov       ecx,ebp
       cmp       rdx,rcx
       ja        near ptr M00_L08
       add       rdi,rax
       mov       ebp,esi
M00_L02:
       mov       rax,rdi
       mov       [rsp+58],rax
       mov       [rsp+60],ebp
       xor       eax,eax
       test      esi,esi
       setne     al
       mov       [rsp+55],al
       xor       ebx,ebx
M00_L03:
       xor       ebp,ebp
       xor       r14d,r14d
M00_L04:
       cmp       byte ptr [rsp+55],0
       je        near ptr M00_L07
       mov       eax,[rsp+60]
       test      eax,eax
       je        near ptr M00_L13
       mov       rdx,[rsp+58]
       movzx     r15d,byte ptr [rdx]
       test      eax,eax
       je        short M00_L08
       inc       rdx
       dec       eax
       mov       [rsp+58],rdx
       mov       [rsp+60],eax
       mov       rax,[rsp+40]
       inc       rax
       mov       [rsp+40],rax
       cmp       dword ptr [rsp+60],0
       je        near ptr M00_L11
M00_L05:
       movzx     r13d,r15b
M00_L06:
       mov       eax,r13d
       and       r13d,7F
       shlx      edx,r13d,r14d
       or        ebp,edx
       add       r14d,7
       cmp       r14d,23
       jg        short M00_L09
       test      al,80
       jne       short M00_L04
       inc       ebx
       cmp       ebx,3E8
       jl        short M00_L03
       mov       rax,[rsp+40]
       add       rsp,90
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r13
       pop       r14
       pop       r15
       ret
M00_L07:
       call      qword ptr [7FF82E5366A0]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
M00_L08:
       call      qword ptr [7FF82E2AC4E0]
       int       3
M00_L09:
       call      qword ptr [7FF82E5368C8]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
M00_L10:
       lea       rdx,[rsp+28]
       mov       rax,[rcx]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rdi,[rsp+28]
       mov       ebp,[rsp+30]
       jmp       near ptr M00_L01
M00_L11:
       cmp       byte ptr [rsp+54],0
       je        short M00_L12
       lea       rcx,[rsp+38]
       call      qword ptr [7FF82E536610]
       movzx     r13d,r15b
       jmp       near ptr M00_L06
M00_L12:
       mov       byte ptr [rsp+55],0
       jmp       near ptr M00_L05
M00_L13:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 467
```

## .NET 9.0.15 (9.0.15, 9.0.1526.17522), X64 RyuJIT x86-64-v3 (Job: net9.0-x64(Platform=X64, Runtime=.NET 9.0))

```asm
; Benchmark.ReadVarInt.ReadByte()
;         var r = new BshoxReader(buffer1);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         for (int i = 0; i < Count; i++)
;              ^^^^^^^^^
;             _ = r.ReadByte();
;             ^^^^^^^^^^^^^^^^^
;         return r.Consumed;
;         ^^^^^^^^^^^^^^^^^^
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,88
       vxorps    xmm4,xmm4,xmm4
       vmovdqu   ymmword ptr [rsp+20],ymm4
       vmovdqu   ymmword ptr [rsp+40],ymm4
       vmovdqu   ymmword ptr [rsp+60],ymm4
       xor       eax,eax
       mov       [rsp+80],rax
       mov       r8,[rcx+38]
       mov       ebx,[rcx+40]
       mov       esi,[rcx+44]
       mov       rax,1DECB800458
       mov       rax,[rax]
       mov       [rsp+30],rax
       xor       eax,eax
       mov       [rsp+38],rax
       mov       byte ptr [rsp+4C],0
       movsxd    rax,esi
       mov       [rsp+40],rax
       xor       edi,edi
       xor       ebp,ebp
       test      r8,r8
       je        short M00_L01
       mov       rax,[r8]
       test      dword ptr [rax],80000000
       je        near ptr M00_L05
       lea       rdi,[r8+10]
       mov       ebp,[r8+8]
M00_L00:
       and       ebx,7FFFFFFF
       mov       eax,ebx
       mov       edx,esi
       add       rdx,rax
       mov       ecx,ebp
       cmp       rdx,rcx
       ja        near ptr M00_L07
       add       rdi,rax
       mov       ebp,esi
M00_L01:
       mov       rax,rdi
       mov       [rsp+50],rax
       mov       [rsp+58],ebp
       test      esi,esi
       setne     al
       movzx     eax,al
       mov       [rsp+4D],al
       mov       edi,3E8
M00_L02:
       cmp       byte ptr [rsp+4D],0
       je        near ptr M00_L08
       mov       eax,[rsp+58]
       test      eax,eax
       je        near ptr M00_L09
       mov       rdx,[rsp+50]
       cmp       [rdx],dl
       test      eax,eax
       je        short M00_L07
       inc       rdx
       dec       eax
       mov       [rsp+50],rdx
       mov       [rsp+58],eax
       mov       rax,[rsp+38]
       inc       rax
       mov       [rsp+38],rax
       cmp       dword ptr [rsp+58],0
       je        short M00_L04
M00_L03:
       dec       edi
       jne       short M00_L02
       mov       rax,[rsp+38]
       add       rsp,88
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M00_L04:
       cmp       byte ptr [rsp+4C],0
       jne       short M00_L06
       mov       byte ptr [rsp+4D],0
       jmp       short M00_L03
M00_L05:
       lea       rdx,[rsp+20]
       mov       rcx,r8
       mov       rax,[r8]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rdi,[rsp+20]
       mov       ebp,[rsp+28]
       jmp       near ptr M00_L00
M00_L06:
       lea       rcx,[rsp+30]
       call      qword ptr [7FF82E43FB28]
       jmp       short M00_L03
M00_L07:
       call      qword ptr [7FF82E2773A8]
       int       3
M00_L08:
       call      qword ptr [7FF82E43FAE0]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M00_L09:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 357
```

## .NET 9.0.15 (9.0.15, 9.0.1526.17522), X64 RyuJIT x86-64-v3 (Job: net9.0-x64(Platform=X64, Runtime=.NET 9.0))

```asm
; Benchmark.ReadVarInt.Read1()
;         var r = new BshoxReader(buffer1);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         for (int i = 0; i < Count; i++)
;              ^^^^^^^^^
;             _ = r.ReadVarInt32();
;             ^^^^^^^^^^^^^^^^^^^^^
;         return r.Consumed;
;         ^^^^^^^^^^^^^^^^^^
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,88
       vxorps    xmm4,xmm4,xmm4
       vmovdqu   ymmword ptr [rsp+20],ymm4
       vmovdqu   ymmword ptr [rsp+40],ymm4
       vmovdqu   ymmword ptr [rsp+60],ymm4
       xor       eax,eax
       mov       [rsp+80],rax
       mov       r8,[rcx+38]
       mov       ebx,[rcx+40]
       mov       esi,[rcx+44]
       mov       rax,1BCCA400458
       mov       rax,[rax]
       mov       [rsp+30],rax
       xor       eax,eax
       mov       [rsp+38],rax
       mov       byte ptr [rsp+4C],0
       movsxd    rax,esi
       mov       [rsp+40],rax
       xor       edi,edi
       xor       ebp,ebp
       test      r8,r8
       je        short M00_L01
       mov       rax,[r8]
       test      dword ptr [rax],80000000
       je        near ptr M00_L05
       lea       rdi,[r8+10]
       mov       ebp,[r8+8]
M00_L00:
       and       ebx,7FFFFFFF
       mov       eax,ebx
       mov       edx,esi
       add       rdx,rax
       mov       ecx,ebp
       cmp       rdx,rcx
       ja        near ptr M00_L09
       add       rdi,rax
       mov       ebp,esi
M00_L01:
       mov       rax,rdi
       mov       [rsp+50],rax
       mov       [rsp+58],ebp
       test      esi,esi
       setne     al
       movzx     eax,al
       mov       [rsp+4D],al
       mov       edi,3E8
M00_L02:
       xor       ebx,ebx
M00_L03:
       cmp       byte ptr [rsp+4D],0
       je        near ptr M00_L10
       mov       eax,[rsp+58]
       test      eax,eax
       je        near ptr M00_L11
       mov       rdx,[rsp+50]
       movzx     ebp,byte ptr [rdx]
       test      eax,eax
       je        near ptr M00_L09
       inc       rdx
       dec       eax
       mov       [rsp+50],rdx
       mov       [rsp+58],eax
       mov       rax,[rsp+38]
       inc       rax
       mov       [rsp+38],rax
       cmp       dword ptr [rsp+58],0
       je        short M00_L06
M00_L04:
       movzx     eax,bpl
       add       ebx,7
       cmp       ebx,23
       jg        short M00_L08
       test      al,80
       jne       short M00_L03
       dec       edi
       jne       short M00_L02
       mov       rax,[rsp+38]
       add       rsp,88
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M00_L05:
       lea       rdx,[rsp+20]
       mov       rcx,r8
       mov       rax,[r8]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rdi,[rsp+20]
       mov       ebp,[rsp+28]
       jmp       near ptr M00_L00
M00_L06:
       cmp       byte ptr [rsp+4C],0
       jne       short M00_L07
       mov       byte ptr [rsp+4D],0
       jmp       short M00_L04
M00_L07:
       lea       rcx,[rsp+30]
       call      qword ptr [7FF82E45FA80]
       jmp       short M00_L04
M00_L08:
       call      qword ptr [7FF82E45FA20]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M00_L09:
       call      qword ptr [7FF82E2973A8]
       int       3
M00_L10:
       call      qword ptr [7FF82E45FA38]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M00_L11:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 396
```

## .NET 9.0.15 (9.0.15, 9.0.1526.17522), X64 RyuJIT x86-64-v3 (Job: net9.0-x64(Platform=X64, Runtime=.NET 9.0))

```asm
; Benchmark.ReadVarInt.ReadAny()
;         var r = new BshoxReader(bufferX);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         for (int i = 0; i < Count; i++)
;              ^^^^^^^^^
;             _ = r.ReadVarInt32();
;             ^^^^^^^^^^^^^^^^^^^^^
;         return r.Consumed;
;         ^^^^^^^^^^^^^^^^^^
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,88
       vxorps    xmm4,xmm4,xmm4
       vmovdqu   ymmword ptr [rsp+20],ymm4
       vmovdqu   ymmword ptr [rsp+40],ymm4
       vmovdqu   ymmword ptr [rsp+60],ymm4
       xor       eax,eax
       mov       [rsp+80],rax
       mov       r8,[rcx+88]
       mov       ebx,[rcx+90]
       mov       esi,[rcx+94]
       mov       rax,16F3D000458
       mov       rax,[rax]
       mov       [rsp+30],rax
       xor       eax,eax
       mov       [rsp+38],rax
       mov       byte ptr [rsp+4C],0
       movsxd    rax,esi
       mov       [rsp+40],rax
       xor       edi,edi
       xor       ebp,ebp
       test      r8,r8
       je        short M00_L01
       mov       rax,[r8]
       test      dword ptr [rax],80000000
       je        near ptr M00_L05
       lea       rdi,[r8+10]
       mov       ebp,[r8+8]
M00_L00:
       and       ebx,7FFFFFFF
       mov       eax,ebx
       mov       edx,esi
       add       rdx,rax
       mov       ecx,ebp
       cmp       rdx,rcx
       ja        near ptr M00_L09
       add       rdi,rax
       mov       ebp,esi
M00_L01:
       mov       rax,rdi
       mov       [rsp+50],rax
       mov       [rsp+58],ebp
       test      esi,esi
       setne     al
       movzx     eax,al
       mov       [rsp+4D],al
       mov       edi,3E8
M00_L02:
       xor       ebx,ebx
M00_L03:
       cmp       byte ptr [rsp+4D],0
       je        near ptr M00_L10
       mov       eax,[rsp+58]
       test      eax,eax
       je        near ptr M00_L11
       mov       rdx,[rsp+50]
       movzx     ebp,byte ptr [rdx]
       test      eax,eax
       je        near ptr M00_L09
       inc       rdx
       dec       eax
       mov       [rsp+50],rdx
       mov       [rsp+58],eax
       mov       rax,[rsp+38]
       inc       rax
       mov       [rsp+38],rax
       cmp       dword ptr [rsp+58],0
       je        short M00_L06
M00_L04:
       movzx     eax,bpl
       add       ebx,7
       cmp       ebx,23
       jg        short M00_L08
       test      al,80
       jne       short M00_L03
       dec       edi
       jne       short M00_L02
       mov       rax,[rsp+38]
       add       rsp,88
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M00_L05:
       lea       rdx,[rsp+20]
       mov       rcx,r8
       mov       rax,[r8]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rdi,[rsp+20]
       mov       ebp,[rsp+28]
       jmp       near ptr M00_L00
M00_L06:
       cmp       byte ptr [rsp+4C],0
       jne       short M00_L07
       mov       byte ptr [rsp+4D],0
       jmp       short M00_L04
M00_L07:
       lea       rcx,[rsp+30]
       call      qword ptr [7FF82E43FA80]
       jmp       short M00_L04
M00_L08:
       call      qword ptr [7FF82E43FA20]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M00_L09:
       call      qword ptr [7FF82E2773A8]
       int       3
M00_L10:
       call      qword ptr [7FF82E43FA38]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M00_L11:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 405
```

## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: net10.0-x64(Platform=X64, Runtime=.NET 10.0))

```asm
; Benchmark.ReadVarInt.ReadByte()
;         var r = new BshoxReader(buffer1);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         for (int i = 0; i < Count; i++)
;              ^^^^^^^^^
;             _ = r.ReadByte();
;             ^^^^^^^^^^^^^^^^^
;         return r.Consumed;
;         ^^^^^^^^^^^^^^^^^^
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,88
       vxorps    xmm4,xmm4,xmm4
       vmovdqu   ymmword ptr [rsp+20],ymm4
       vmovdqu   ymmword ptr [rsp+40],ymm4
       vmovdqu   ymmword ptr [rsp+60],ymm4
       xor       eax,eax
       mov       [rsp+80],rax
       mov       rax,[rcx+38]
       mov       ebx,[rcx+40]
       mov       esi,[rcx+44]
       mov       rdx,208F4C00458
       mov       rdx,[rdx]
       mov       [rsp+30],rdx
       xor       edx,edx
       mov       [rsp+38],rdx
       mov       byte ptr [rsp+4C],0
       movsxd    rdx,esi
       mov       [rsp+40],rdx
       xor       edi,edi
       xor       ebp,ebp
       test      rax,rax
       je        short M00_L01
       mov       rdx,[rax]
       test      dword ptr [rdx],80000000
       je        near ptr M00_L06
       lea       rdi,[rax+10]
       mov       ebp,[rax+8]
M00_L00:
       and       ebx,7FFFFFFF
       mov       eax,ebx
       mov       ecx,esi
       add       rcx,rax
       mov       edx,ebp
       cmp       rcx,rdx
       ja        near ptr M00_L09
       add       rdi,rax
       mov       ebp,esi
M00_L01:
       mov       [rsp+50],rdi
       mov       [rsp+58],ebp
       test      esi,esi
       setne     cl
       movzx     ecx,cl
       mov       [rsp+4D],cl
       cmp       byte ptr [rsp+4D],0
       je        near ptr M00_L08
       mov       ebx,3E8
M00_L02:
       mov       ecx,[rsp+58]
       test      ecx,ecx
       je        near ptr M00_L10
       mov       rax,[rsp+50]
       cmp       [rax],al
       inc       rax
       dec       ecx
       mov       [rsp+50],rax
       mov       [rsp+58],ecx
       mov       rcx,[rsp+38]
       inc       rcx
       mov       [rsp+38],rcx
       cmp       dword ptr [rsp+58],0
       je        short M00_L04
M00_L03:
       dec       ebx
       je        short M00_L05
       cmp       byte ptr [rsp+4D],0
       jne       short M00_L02
       jmp       short M00_L08
M00_L04:
       cmp       byte ptr [rsp+4C],0
       jne       short M00_L07
       mov       byte ptr [rsp+4D],0
       jmp       short M00_L03
M00_L05:
       mov       rax,[rsp+38]
       add       rsp,88
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M00_L06:
       lea       rdx,[rsp+20]
       mov       rcx,rax
       mov       rax,[rax]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rdi,[rsp+20]
       mov       ebp,[rsp+28]
       jmp       near ptr M00_L00
M00_L07:
       lea       rcx,[rsp+30]
       call      qword ptr [7FF8309B4A68]
       jmp       short M00_L03
M00_L08:
       call      qword ptr [7FF8309B4A20]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M00_L09:
       call      qword ptr [7FF8307A7990]
       int       3
M00_L10:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 358
```

## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: net10.0-x64(Platform=X64, Runtime=.NET 10.0))

```asm
; Benchmark.ReadVarInt.Read1()
;         var r = new BshoxReader(buffer1);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         for (int i = 0; i < Count; i++)
;              ^^^^^^^^^
;             _ = r.ReadVarInt32();
;             ^^^^^^^^^^^^^^^^^^^^^
;         return r.Consumed;
;         ^^^^^^^^^^^^^^^^^^
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,88
       vxorps    xmm4,xmm4,xmm4
       vmovdqu   ymmword ptr [rsp+20],ymm4
       vmovdqu   ymmword ptr [rsp+40],ymm4
       vmovdqu   ymmword ptr [rsp+60],ymm4
       xor       eax,eax
       mov       [rsp+80],rax
       mov       rax,[rcx+38]
       mov       ebx,[rcx+40]
       mov       esi,[rcx+44]
       mov       rdx,1DF69000458
       mov       rdx,[rdx]
       mov       [rsp+30],rdx
       xor       edx,edx
       mov       [rsp+38],rdx
       mov       byte ptr [rsp+4C],0
       movsxd    rdx,esi
       mov       [rsp+40],rdx
       xor       edi,edi
       xor       ebp,ebp
       test      rax,rax
       je        short M00_L01
       mov       rdx,[rax]
       test      dword ptr [rdx],80000000
       je        near ptr M00_L06
       lea       rdi,[rax+10]
       mov       ebp,[rax+8]
M00_L00:
       and       ebx,7FFFFFFF
       mov       eax,ebx
       mov       ecx,esi
       add       rcx,rax
       mov       edx,ebp
       cmp       rcx,rdx
       ja        near ptr M00_L10
       add       rdi,rax
       mov       ebp,esi
M00_L01:
       mov       [rsp+50],rdi
       mov       [rsp+58],ebp
       test      esi,esi
       setne     cl
       movzx     ecx,cl
       mov       [rsp+4D],cl
       mov       ebx,3E8
M00_L02:
       xor       esi,esi
       cmp       byte ptr [rsp+4D],0
       je        near ptr M00_L09
M00_L03:
       mov       ecx,[rsp+58]
       test      ecx,ecx
       je        near ptr M00_L12
       mov       rax,[rsp+50]
       movzx     edi,byte ptr [rax]
       inc       rax
       dec       ecx
       mov       [rsp+50],rax
       mov       [rsp+58],ecx
       mov       rcx,[rsp+38]
       inc       rcx
       mov       [rsp+38],rcx
       cmp       dword ptr [rsp+58],0
       je        short M00_L05
M00_L04:
       movzx     eax,dil
       add       esi,7
       cmp       esi,23
       jg        short M00_L11
       test      al,80
       jne       short M00_L07
       dec       ebx
       jne       short M00_L02
       mov       rax,[rsp+38]
       add       rsp,88
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M00_L05:
       cmp       byte ptr [rsp+4C],0
       jne       short M00_L08
       mov       byte ptr [rsp+4D],0
       jmp       short M00_L04
M00_L06:
       lea       rdx,[rsp+20]
       mov       rcx,rax
       mov       rax,[rax]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rdi,[rsp+20]
       mov       ebp,[rsp+28]
       jmp       near ptr M00_L00
M00_L07:
       cmp       byte ptr [rsp+4D],0
       jne       near ptr M00_L03
       jmp       short M00_L09
M00_L08:
       lea       rcx,[rsp+30]
       call      qword ptr [7FF830994A80]
       jmp       short M00_L04
M00_L09:
       call      qword ptr [7FF830994A38]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M00_L10:
       call      qword ptr [7FF830787990]
       int       3
M00_L11:
       call      qword ptr [7FF830994A20]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M00_L12:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 397
```

## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: net10.0-x64(Platform=X64, Runtime=.NET 10.0))

```asm
; Benchmark.ReadVarInt.ReadAny()
;         var r = new BshoxReader(bufferX);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;         for (int i = 0; i < Count; i++)
;              ^^^^^^^^^
;             _ = r.ReadVarInt32();
;             ^^^^^^^^^^^^^^^^^^^^^
;         return r.Consumed;
;         ^^^^^^^^^^^^^^^^^^
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,88
       vxorps    xmm4,xmm4,xmm4
       vmovdqu   ymmword ptr [rsp+20],ymm4
       vmovdqu   ymmword ptr [rsp+40],ymm4
       vmovdqu   ymmword ptr [rsp+60],ymm4
       xor       eax,eax
       mov       [rsp+80],rax
       mov       rax,[rcx+88]
       mov       ebx,[rcx+90]
       mov       esi,[rcx+94]
       mov       rdx,29ADE000458
       mov       rdx,[rdx]
       mov       [rsp+30],rdx
       xor       edx,edx
       mov       [rsp+38],rdx
       mov       byte ptr [rsp+4C],0
       movsxd    rdx,esi
       mov       [rsp+40],rdx
       xor       edi,edi
       xor       ebp,ebp
       test      rax,rax
       je        short M00_L01
       mov       rdx,[rax]
       test      dword ptr [rdx],80000000
       je        near ptr M00_L07
       lea       rdi,[rax+10]
       mov       ebp,[rax+8]
M00_L00:
       and       ebx,7FFFFFFF
       mov       eax,ebx
       mov       ecx,esi
       add       rcx,rax
       mov       edx,ebp
       cmp       rcx,rdx
       ja        near ptr M00_L11
       add       rdi,rax
       mov       ebp,esi
M00_L01:
       mov       [rsp+50],rdi
       mov       [rsp+58],ebp
       test      esi,esi
       setne     cl
       movzx     ecx,cl
       mov       [rsp+4D],cl
       mov       ebx,3E8
       jmp       short M00_L03
M00_L02:
       dec       ebx
       je        short M00_L06
M00_L03:
       xor       esi,esi
       cmp       byte ptr [rsp+4D],0
       je        near ptr M00_L10
M00_L04:
       mov       ecx,[rsp+58]
       test      ecx,ecx
       je        near ptr M00_L13
       mov       rax,[rsp+50]
       movzx     edi,byte ptr [rax]
       inc       rax
       dec       ecx
       mov       [rsp+50],rax
       mov       [rsp+58],ecx
       mov       rcx,[rsp+38]
       inc       rcx
       mov       [rsp+38],rcx
       cmp       dword ptr [rsp+58],0
       je        short M00_L08
M00_L05:
       movzx     eax,dil
       add       esi,7
       cmp       esi,23
       jg        short M00_L12
       test      al,80
       je        short M00_L02
       cmp       byte ptr [rsp+4D],0
       jne       short M00_L04
       jmp       short M00_L10
M00_L06:
       mov       rax,[rsp+38]
       add       rsp,88
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M00_L07:
       lea       rdx,[rsp+20]
       mov       rcx,rax
       mov       rax,[rax]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rdi,[rsp+20]
       mov       ebp,[rsp+28]
       jmp       near ptr M00_L00
M00_L08:
       cmp       byte ptr [rsp+4C],0
       je        short M00_L09
       lea       rcx,[rsp+30]
       call      qword ptr [7FF830984A68]
       jmp       short M00_L05
M00_L09:
       mov       byte ptr [rsp+4D],0
       jmp       short M00_L05
M00_L10:
       call      qword ptr [7FF830984A20]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M00_L11:
       call      qword ptr [7FF830777990]
       int       3
M00_L12:
       call      qword ptr [7FF830984A08]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M00_L13:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 404
```

## .NET Framework 4.8.1 (4.8.9325.0), X64 RyuJIT VectorSize=256 (Job: net48-x64(Platform=X64, Runtime=.NET Framework 4.8))

```asm
; Benchmark.ReadVarInt.ReadByte()
       push      rdi
       push      rsi
       sub       rsp,98
       vzeroupper
       mov       rsi,rcx
       lea       rdi,[rsp+20]
       mov       ecx,1E
       xor       eax,eax
       rep stosd
       mov       rcx,rsi
;         var r = new BshoxReader(buffer1);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       lea       rdx,[rsp+30]
       add       rcx,38
       vmovdqu   xmm0,xmmword ptr [rcx]
       vmovdqu   xmmword ptr [rsp+20],xmm0
       mov       rcx,rdx
       lea       rdx,[rsp+20]
       xor       r8d,r8d
       call      Bshox.BshoxReader..ctor(System.ReadOnlyMemory`1<Byte>, Bshox.BshoxOptions)
;         for (int i = 0; i < Count; i++)
;              ^^^^^^^^^
       xor       esi,esi
;             _ = r.ReadByte();
;             ^^^^^^^^^^^^^^^^^
M00_L00:
       cmp       byte ptr [rsp+4D],0
       je        near ptr M00_L05
       cmp       dword ptr [rsp+60],0
       jbe       near ptr M00_L06
       cmp       qword ptr [rsp+50],0
       jne       short M00_L01
       mov       rcx,[rsp+58]
       jmp       short M00_L02
M00_L01:
       mov       rcx,[rsp+50]
       cmp       [rcx],ecx
       add       rcx,8
       mov       rax,[rsp+58]
       add       rcx,rax
M00_L02:
       movzx     ecx,byte ptr [rcx]
       cmp       dword ptr [rsp+60],1
       jb        near ptr M00_L07
       mov       rcx,[rsp+58]
       inc       rcx
       mov       eax,[rsp+60]
       dec       eax
       mov       rdx,[rsp+50]
       lea       r8,[rsp+50]
       mov       [r8],rdx
       mov       [r8+8],rcx
       mov       [r8+10],eax
       mov       rcx,[rsp+38]
       inc       rcx
       mov       [rsp+38],rcx
       cmp       dword ptr [rsp+60],0
       jne       short M00_L04
       cmp       byte ptr [rsp+4C],0
       je        short M00_L03
       lea       rcx,[rsp+30]
       call      00007FF84207F540
       jmp       short M00_L04
M00_L03:
       mov       byte ptr [rsp+4D],0
M00_L04:
       inc       esi
       cmp       esi,3E8
       jl        near ptr M00_L00
       mov       rax,[rsp+38]
       add       rsp,98
       pop       rsi
       pop       rdi
       ret
M00_L05:
       call      00007FF84207F570
       mov       rcx,rax
       call      CORINFO_HELP_THROW
M00_L06:
       call      00007FF84206E584
       int       3
M00_L07:
       mov       ecx,1
       call      00007FF84206E560
       int       3
; Total bytes of code 279
```
```asm
; Bshox.BshoxReader..ctor(System.ReadOnlyMemory`1<Byte>, Bshox.BshoxOptions)
       push      r15
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,48
       mov       rsi,rcx
       lea       rdi,[rsp+30]
       mov       ecx,6
       xor       eax,eax
       rep stosd
       mov       rcx,rsi
       mov       rsi,rcx
       mov       rdi,[rdx]
       mov       ebx,[rdx+8]
       mov       ebp,[rdx+0C]
;     public BshoxReader(ReadOnlyMemory<byte> memory, BshoxOptions? options = null) : this(options)
;                                                                                     ^^^^^^^^^^^^^
       mov       rcx,rsi
       mov       rdx,r8
       call      Bshox.BshoxReader..ctor(Bshox.BshoxOptions)
;         Consumed = 0;
;         ^^^^^^^^^^^^^
       xor       ecx,ecx
       mov       [rsi+8],rcx
;         _usingSequence = false;
;         ^^^^^^^^^^^^^^^^^^^^^^^
       mov       byte ptr [rsi+1C],0
       mov       ecx,ebp
       and       ecx,7FFFFFFF
       movsxd    rcx,ecx
       mov       [rsi+10],rcx
;         _span = memory.Span;
;         ^^^^^^^^^^^^^^^^^^^^
       test      ebx,ebx
       jge       short M01_L01
       mov       rcx,rdi
       test      rcx,rcx
       je        short M01_L00
       mov       rdx,offset MT_System.Buffers.MemoryManager<System.Byte>
       cmp       [rcx],rdx
       je        short M01_L00
       mov       rcx,rdx
       mov       rdx,rdi
       call      CORINFO_HELP_CHKCASTCLASS_SPECIAL
       mov       rcx,rax
M01_L00:
       lea       rdx,[rsp+30]
       mov       rax,[rcx]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       edx,ebx
       and       edx,7FFFFFFF
       cmp       edx,[rsp+40]
       ja        near ptr M01_L04
       mov       ecx,[rsp+40]
       sub       ecx,edx
       cmp       ecx,ebp
       jb        near ptr M01_L04
       mov       rcx,[rsp+38]
       movsxd    rdx,edx
       add       rdx,rcx
       mov       rcx,[rsp+30]
       mov       r14,rdx
       mov       r15d,ebp
       mov       [rsp+28],rcx
       jmp       short M01_L03
M01_L01:
       test      rdi,rdi
       je        short M01_L02
       mov       rdx,rdi
       mov       rcx,7FF8A0074FCA
       call      CORINFO_HELP_CHKCASTARRAY
       mov       edx,ebp
       and       edx,7FFFFFFF
       cmp       [rax+8],ebx
       jb        short M01_L05
       mov       ecx,[rax+8]
       sub       ecx,ebx
       cmp       ecx,edx
       jb        short M01_L05
       mov       r15d,edx
       mov       rcx,rax
       movsxd    rdx,ebx
       add       rdx,8
       mov       r14,rdx
       mov       [rsp+28],rcx
       jmp       short M01_L03
M01_L02:
       xor       ecx,ecx
       xor       r14d,r14d
       xor       r15d,r15d
       mov       [rsp+28],rcx
M01_L03:
       lea       rdi,[rsi+20]
       lea       rcx,[rdi]
       mov       rdx,[rsp+28]
       call      CORINFO_HELP_CHECKED_ASSIGN_REF
       mov       [rdi+8],r14
       mov       [rdi+10],r15d
       test      ebp,7FFFFFFF
       setne     cl
       mov       [rsi+1D],cl
       add       rsp,48
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       pop       r15
       ret
M01_L04:
       mov       ecx,1
       call      00007FF84206E560
       int       3
M01_L05:
       mov       ecx,1
       call      00007FF84206E560
       int       3
; Total bytes of code 350
```

## .NET Framework 4.8.1 (4.8.9325.0), X64 RyuJIT VectorSize=256 (Job: net48-x64(Platform=X64, Runtime=.NET Framework 4.8))

```asm
; Benchmark.ReadVarInt.Read1()
       push      rdi
       push      rsi
       sub       rsp,98
       vzeroupper
       mov       rsi,rcx
       lea       rdi,[rsp+20]
       mov       ecx,1E
       xor       eax,eax
       rep stosd
       mov       rcx,rsi
;         var r = new BshoxReader(buffer1);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       lea       rdx,[rsp+30]
       add       rcx,38
       vmovdqu   xmm0,xmmword ptr [rcx]
       vmovdqu   xmmword ptr [rsp+20],xmm0
       mov       rcx,rdx
       lea       rdx,[rsp+20]
       xor       r8d,r8d
       call      Bshox.BshoxReader..ctor(System.ReadOnlyMemory`1<Byte>, Bshox.BshoxOptions)
;         for (int i = 0; i < Count; i++)
;              ^^^^^^^^^
       xor       esi,esi
M00_L00:
       lea       rcx,[rsp+30]
       call      Bshox.BshoxReader.ReadVarInt32()
       inc       esi
       cmp       esi,3E8
       jl        short M00_L00
       mov       rax,[rsp+38]
       add       rsp,98
       pop       rsi
       pop       rdi
       ret
; Total bytes of code 106
```
```asm
; Bshox.BshoxReader..ctor(System.ReadOnlyMemory`1<Byte>, Bshox.BshoxOptions)
       push      r15
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,48
       mov       rsi,rcx
       lea       rdi,[rsp+30]
       mov       ecx,6
       xor       eax,eax
       rep stosd
       mov       rcx,rsi
       mov       rsi,rcx
       mov       rdi,[rdx]
       mov       ebx,[rdx+8]
       mov       ebp,[rdx+0C]
;     public BshoxReader(ReadOnlyMemory<byte> memory, BshoxOptions? options = null) : this(options)
;                                                                                     ^^^^^^^^^^^^^
       mov       rcx,rsi
       mov       rdx,r8
       call      Bshox.BshoxReader..ctor(Bshox.BshoxOptions)
;         Consumed = 0;
;         ^^^^^^^^^^^^^
       xor       ecx,ecx
       mov       [rsi+8],rcx
;         _usingSequence = false;
;         ^^^^^^^^^^^^^^^^^^^^^^^
       mov       byte ptr [rsi+1C],0
       mov       ecx,ebp
       and       ecx,7FFFFFFF
       movsxd    rcx,ecx
       mov       [rsi+10],rcx
;         _span = memory.Span;
;         ^^^^^^^^^^^^^^^^^^^^
       test      ebx,ebx
       jge       short M01_L01
       mov       rcx,rdi
       test      rcx,rcx
       je        short M01_L00
       mov       rdx,offset MT_System.Buffers.MemoryManager<System.Byte>
       cmp       [rcx],rdx
       je        short M01_L00
       mov       rcx,rdx
       mov       rdx,rdi
       call      CORINFO_HELP_CHKCASTCLASS_SPECIAL
       mov       rcx,rax
M01_L00:
       lea       rdx,[rsp+30]
       mov       rax,[rcx]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       edx,ebx
       and       edx,7FFFFFFF
       cmp       edx,[rsp+40]
       ja        near ptr M01_L04
       mov       ecx,[rsp+40]
       sub       ecx,edx
       cmp       ecx,ebp
       jb        near ptr M01_L04
       mov       rcx,[rsp+38]
       movsxd    rdx,edx
       add       rdx,rcx
       mov       rcx,[rsp+30]
       mov       r14,rdx
       mov       r15d,ebp
       mov       [rsp+28],rcx
       jmp       short M01_L03
M01_L01:
       test      rdi,rdi
       je        short M01_L02
       mov       rdx,rdi
       mov       rcx,7FF8A0074FCA
       call      CORINFO_HELP_CHKCASTARRAY
       mov       edx,ebp
       and       edx,7FFFFFFF
       cmp       [rax+8],ebx
       jb        short M01_L05
       mov       ecx,[rax+8]
       sub       ecx,ebx
       cmp       ecx,edx
       jb        short M01_L05
       mov       r15d,edx
       mov       rcx,rax
       movsxd    rdx,ebx
       add       rdx,8
       mov       r14,rdx
       mov       [rsp+28],rcx
       jmp       short M01_L03
M01_L02:
       xor       ecx,ecx
       xor       r14d,r14d
       xor       r15d,r15d
       mov       [rsp+28],rcx
M01_L03:
       lea       rdi,[rsi+20]
       lea       rcx,[rdi]
       mov       rdx,[rsp+28]
       call      CORINFO_HELP_CHECKED_ASSIGN_REF
       mov       [rdi+8],r14
       mov       [rdi+10],r15d
       test      ebp,7FFFFFFF
       setne     cl
       mov       [rsi+1D],cl
       add       rsp,48
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       pop       r15
       ret
M01_L04:
       mov       ecx,1
       call      00007FF84203E560
       int       3
M01_L05:
       mov       ecx,1
       call      00007FF84203E560
       int       3
; Total bytes of code 350
```
```asm
; Bshox.BshoxReader.ReadVarInt32()
       push      r15
       push      r14
       push      r13
       push      r12
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,38
       mov       rsi,rcx
;         uint value = 0;
;         ^^^^^^^^^^^^^^^
       xor       edi,edi
;         int shift = 0;
;         ^^^^^^^^^^^^^^
       xor       ebx,ebx
;             b = ReadByte();
;             ^^^^^^^^^^^^^^^
M02_L00:
       cmp       byte ptr [rsi+1D],0
       je        near ptr M02_L06
       cmp       [rsi],esi
       lea       rbp,[rsi+20]
       mov       rdx,rbp
       mov       ecx,[rdx+10]
       test      ecx,ecx
       je        near ptr M02_L07
       mov       rax,[rdx]
       test      rax,rax
       jne       short M02_L01
       mov       r8,[rdx+8]
       mov       rdx,r8
       mov       r9,rdx
       mov       rdx,r9
       jmp       short M02_L02
M02_L01:
       mov       r8,rax
       cmp       [r8],r8d
       lea       r9,[r8+8]
       mov       r8,[rdx+8]
       mov       rdx,r8
       add       rdx,r9
M02_L02:
       movzx     r14d,byte ptr [rdx]
       cmp       [rsi],esi
       cmp       ecx,1
       jb        near ptr M02_L08
       lea       rdx,[r8+1]
       mov       r15,rdx
       mov       rdx,r15
       lea       r15d,[rcx-1]
       mov       r12,rdx
       mov       r13,rbp
       mov       rcx,r13
       mov       rdx,rax
       call      CORINFO_HELP_CHECKED_ASSIGN_REF
       mov       [r13+8],r12
       mov       [r13+10],r15d
       mov       rcx,[rsi+8]
       inc       rcx
       mov       [rsi+8],rcx
       cmp       [rsi],esi
       cmp       dword ptr [rbp+10],0
       jne       short M02_L04
       cmp       byte ptr [rsi+1C],0
       je        short M02_L03
       mov       rcx,rsi
       call      00007FF84204F540
       movzx     ecx,r14b
       jmp       short M02_L05
M02_L03:
       mov       byte ptr [rsi+1D],0
M02_L04:
       movzx     ecx,r14b
;             value |= (uint)(b & 0x7F) << shift;
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M02_L05:
       mov       [rsp+34],ecx
       mov       eax,ecx
       and       eax,7F
       mov       ecx,ebx
       shl       eax,cl
       or        edi,eax
;             shift += 7;
;             ^^^^^^^^^^^
       add       ebx,7
;             if (shift > 5 * 7)
;             ^^^^^^^^^^^^^^^^^^
       cmp       ebx,23
       jg        short M02_L09
;         } while ((b & 0x80) != 0);
;           ^^^^^^^^^^^^^^^^^^^^^^^^
       test      byte ptr [rsp+34],80
       jne       near ptr M02_L00
;         return value;
;         ^^^^^^^^^^^^^
       mov       eax,edi
       add       rsp,38
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r12
       pop       r13
       pop       r14
       pop       r15
       ret
M02_L06:
       call      00007FF84204F570
       mov       rcx,rax
       call      CORINFO_HELP_THROW
M02_L07:
       call      00007FF84203E584
       int       3
M02_L08:
       mov       ecx,1
       call      00007FF84203E560
       int       3
;                 throw BshoxException.VarIntTooLong();
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M02_L09:
       mov       rcx,offset MT_Bshox.BshoxException
       call      CORINFO_HELP_NEWSFAST
       mov       rsi,rax
       mov       rcx,rsi
       call      System.Exception.Init()
       mov       ecx,109
       mov       rdx,7FF842141660
       call      CORINFO_HELP_STRCNS
       lea       rcx,[rsi+20]
       mov       rdx,rax
       call      CORINFO_HELP_ASSIGN_REF
       mov       rcx,rsi
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 347
```

## .NET Framework 4.8.1 (4.8.9325.0), X64 RyuJIT VectorSize=256 (Job: net48-x64(Platform=X64, Runtime=.NET Framework 4.8))

```asm
; Benchmark.ReadVarInt.ReadAny()
       push      rdi
       push      rsi
       sub       rsp,98
       vzeroupper
       mov       rsi,rcx
       lea       rdi,[rsp+20]
       mov       ecx,1E
       xor       eax,eax
       rep stosd
       mov       rcx,rsi
;         var r = new BshoxReader(bufferX);
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       lea       rdx,[rsp+30]
       add       rcx,88
       vmovdqu   xmm0,xmmword ptr [rcx]
       vmovdqu   xmmword ptr [rsp+20],xmm0
       mov       rcx,rdx
       lea       rdx,[rsp+20]
       xor       r8d,r8d
       call      Bshox.BshoxReader..ctor(System.ReadOnlyMemory`1<Byte>, Bshox.BshoxOptions)
;         for (int i = 0; i < Count; i++)
;              ^^^^^^^^^
       xor       esi,esi
M00_L00:
       lea       rcx,[rsp+30]
       call      Bshox.BshoxReader.ReadVarInt32()
       inc       esi
       cmp       esi,3E8
       jl        short M00_L00
       mov       rax,[rsp+38]
       add       rsp,98
       pop       rsi
       pop       rdi
       ret
; Total bytes of code 109
```
```asm
; Bshox.BshoxReader..ctor(System.ReadOnlyMemory`1<Byte>, Bshox.BshoxOptions)
       push      r15
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,48
       mov       rsi,rcx
       lea       rdi,[rsp+30]
       mov       ecx,6
       xor       eax,eax
       rep stosd
       mov       rcx,rsi
       mov       rsi,rcx
       mov       rdi,[rdx]
       mov       ebx,[rdx+8]
       mov       ebp,[rdx+0C]
;     public BshoxReader(ReadOnlyMemory<byte> memory, BshoxOptions? options = null) : this(options)
;                                                                                     ^^^^^^^^^^^^^
       mov       rcx,rsi
       mov       rdx,r8
       call      Bshox.BshoxReader..ctor(Bshox.BshoxOptions)
;         Consumed = 0;
;         ^^^^^^^^^^^^^
       xor       ecx,ecx
       mov       [rsi+8],rcx
;         _usingSequence = false;
;         ^^^^^^^^^^^^^^^^^^^^^^^
       mov       byte ptr [rsi+1C],0
       mov       ecx,ebp
       and       ecx,7FFFFFFF
       movsxd    rcx,ecx
       mov       [rsi+10],rcx
;         _span = memory.Span;
;         ^^^^^^^^^^^^^^^^^^^^
       test      ebx,ebx
       jge       short M01_L01
       mov       rcx,rdi
       test      rcx,rcx
       je        short M01_L00
       mov       rdx,offset MT_System.Buffers.MemoryManager<System.Byte>
       cmp       [rcx],rdx
       je        short M01_L00
       mov       rcx,rdx
       mov       rdx,rdi
       call      CORINFO_HELP_CHKCASTCLASS_SPECIAL
       mov       rcx,rax
M01_L00:
       lea       rdx,[rsp+30]
       mov       rax,[rcx]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       edx,ebx
       and       edx,7FFFFFFF
       cmp       edx,[rsp+40]
       ja        near ptr M01_L04
       mov       ecx,[rsp+40]
       sub       ecx,edx
       cmp       ecx,ebp
       jb        near ptr M01_L04
       mov       rcx,[rsp+38]
       movsxd    rdx,edx
       add       rdx,rcx
       mov       rcx,[rsp+30]
       mov       r14,rdx
       mov       r15d,ebp
       mov       [rsp+28],rcx
       jmp       short M01_L03
M01_L01:
       test      rdi,rdi
       je        short M01_L02
       mov       rdx,rdi
       mov       rcx,7FF8A0074FCA
       call      CORINFO_HELP_CHKCASTARRAY
       mov       edx,ebp
       and       edx,7FFFFFFF
       cmp       [rax+8],ebx
       jb        short M01_L05
       mov       ecx,[rax+8]
       sub       ecx,ebx
       cmp       ecx,edx
       jb        short M01_L05
       mov       r15d,edx
       mov       rcx,rax
       movsxd    rdx,ebx
       add       rdx,8
       mov       r14,rdx
       mov       [rsp+28],rcx
       jmp       short M01_L03
M01_L02:
       xor       ecx,ecx
       xor       r14d,r14d
       xor       r15d,r15d
       mov       [rsp+28],rcx
M01_L03:
       lea       rdi,[rsi+20]
       lea       rcx,[rdi]
       mov       rdx,[rsp+28]
       call      CORINFO_HELP_CHECKED_ASSIGN_REF
       mov       [rdi+8],r14
       mov       [rdi+10],r15d
       test      ebp,7FFFFFFF
       setne     cl
       mov       [rsi+1D],cl
       add       rsp,48
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       pop       r15
       ret
M01_L04:
       mov       ecx,1
       call      00007FF84205E560
       int       3
M01_L05:
       mov       ecx,1
       call      00007FF84205E560
       int       3
; Total bytes of code 350
```
```asm
; Bshox.BshoxReader.ReadVarInt32()
       push      r15
       push      r14
       push      r13
       push      r12
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,38
       mov       rsi,rcx
;         uint value = 0;
;         ^^^^^^^^^^^^^^^
       xor       edi,edi
;         int shift = 0;
;         ^^^^^^^^^^^^^^
       xor       ebx,ebx
;             b = ReadByte();
;             ^^^^^^^^^^^^^^^
M02_L00:
       cmp       byte ptr [rsi+1D],0
       je        near ptr M02_L06
       cmp       [rsi],esi
       lea       rbp,[rsi+20]
       mov       rdx,rbp
       mov       ecx,[rdx+10]
       test      ecx,ecx
       je        near ptr M02_L07
       mov       rax,[rdx]
       test      rax,rax
       jne       short M02_L01
       mov       r8,[rdx+8]
       mov       rdx,r8
       mov       r9,rdx
       mov       rdx,r9
       jmp       short M02_L02
M02_L01:
       mov       r8,rax
       cmp       [r8],r8d
       lea       r9,[r8+8]
       mov       r8,[rdx+8]
       mov       rdx,r8
       add       rdx,r9
M02_L02:
       movzx     r14d,byte ptr [rdx]
       cmp       [rsi],esi
       cmp       ecx,1
       jb        near ptr M02_L08
       lea       rdx,[r8+1]
       mov       r15,rdx
       mov       rdx,r15
       lea       r15d,[rcx-1]
       mov       r12,rdx
       mov       r13,rbp
       mov       rcx,r13
       mov       rdx,rax
       call      CORINFO_HELP_CHECKED_ASSIGN_REF
       mov       [r13+8],r12
       mov       [r13+10],r15d
       mov       rcx,[rsi+8]
       inc       rcx
       mov       [rsi+8],rcx
       cmp       [rsi],esi
       cmp       dword ptr [rbp+10],0
       jne       short M02_L04
       cmp       byte ptr [rsi+1C],0
       je        short M02_L03
       mov       rcx,rsi
       call      00007FF84206F540
       movzx     ecx,r14b
       jmp       short M02_L05
M02_L03:
       mov       byte ptr [rsi+1D],0
M02_L04:
       movzx     ecx,r14b
;             value |= (uint)(b & 0x7F) << shift;
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M02_L05:
       mov       [rsp+34],ecx
       mov       eax,ecx
       and       eax,7F
       mov       ecx,ebx
       shl       eax,cl
       or        edi,eax
;             shift += 7;
;             ^^^^^^^^^^^
       add       ebx,7
;             if (shift > 5 * 7)
;             ^^^^^^^^^^^^^^^^^^
       cmp       ebx,23
       jg        short M02_L09
;         } while ((b & 0x80) != 0);
;           ^^^^^^^^^^^^^^^^^^^^^^^^
       test      byte ptr [rsp+34],80
       jne       near ptr M02_L00
;         return value;
;         ^^^^^^^^^^^^^
       mov       eax,edi
       add       rsp,38
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r12
       pop       r13
       pop       r14
       pop       r15
       ret
M02_L06:
       call      00007FF84206F570
       mov       rcx,rax
       call      CORINFO_HELP_THROW
M02_L07:
       call      00007FF84205E584
       int       3
M02_L08:
       mov       ecx,1
       call      00007FF84205E560
       int       3
;                 throw BshoxException.VarIntTooLong();
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M02_L09:
       mov       rcx,offset MT_Bshox.BshoxException
       call      CORINFO_HELP_NEWSFAST
       mov       rsi,rax
       mov       rcx,rsi
       call      System.Exception.Init()
       mov       ecx,109
       mov       rdx,7FF842161660
       call      CORINFO_HELP_STRCNS
       lea       rcx,[rsi+20]
       mov       rdx,rax
       call      CORINFO_HELP_ASSIGN_REF
       mov       rcx,rsi
       call      CORINFO_HELP_THROW
       int       3
; Total bytes of code 347
```