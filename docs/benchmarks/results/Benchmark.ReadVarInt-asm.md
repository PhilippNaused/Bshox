## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: DefaultJob)

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
       mov       rdx,204348003F0
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
       call      qword ptr [7FF8309847C8]
       jmp       short M00_L03
M00_L08:
       call      qword ptr [7FF830984780]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M00_L09:
       call      qword ptr [7FF830777990]
       int       3
M00_L10:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 358
```

## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: DefaultJob)

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
       mov       rdx,1E0A3C003F0
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
       je        near ptr M00_L05
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
       je        short M00_L07
M00_L04:
       movzx     eax,dil
       add       esi,7
       cmp       esi,23
       jg        short M00_L11
       test      al,80
       jne       short M00_L06
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
       lea       rdx,[rsp+20]
       mov       rcx,rax
       mov       rax,[rax]
       mov       rax,[rax+40]
       call      qword ptr [rax+28]
       mov       rdi,[rsp+20]
       mov       ebp,[rsp+28]
       jmp       near ptr M00_L00
M00_L06:
       cmp       byte ptr [rsp+4D],0
       jne       near ptr M00_L03
       jmp       short M00_L09
M00_L07:
       cmp       byte ptr [rsp+4C],0
       je        short M00_L08
       lea       rcx,[rsp+30]
       call      qword ptr [7FF8309A48D0]
       jmp       short M00_L04
M00_L08:
       mov       byte ptr [rsp+4D],0
       jmp       short M00_L04
M00_L09:
       call      qword ptr [7FF8309A4888]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M00_L10:
       call      qword ptr [7FF830797990]
       int       3
M00_L11:
       call      qword ptr [7FF8309A4870]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M00_L12:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 397
```

## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v3 (Job: DefaultJob)

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
       mov       rdx,180BA8003F0
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
       call      qword ptr [7FF8309948D0]
       jmp       short M00_L05
M00_L09:
       mov       byte ptr [rsp+4D],0
       jmp       short M00_L05
M00_L10:
       call      qword ptr [7FF830994888]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M00_L11:
       call      qword ptr [7FF830787990]
       int       3
M00_L12:
       call      qword ptr [7FF830994870]
       mov       rcx,rax
       call      CORINFO_HELP_THROW
       int       3
M00_L13:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 404
```