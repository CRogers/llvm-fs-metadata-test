mkdir -p build/
fsc -r:../lib/LLVMFsharp.dll --out:build/llvm_fs_metadata_test.exe --platform:x86 --target:exe Program.fs
cp ../lib/*.dll build/
