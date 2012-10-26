open LLVM.Core
open LLVM.Generated.Core
open LLVM.Generated.BitWriter

let i32 = int32Type ()
let i32zero = constInt i32 0UL false

[<EntryPoint>]
let main argv =
    // Set up the module/function
    let module_ = moduleCreateWithName "foobar"
    let context = getModuleContext module_
    let funcTy = functionType i32 [||]
    let func = addFunction module_ "main" funcTy
    let bldr = createBuilder ()
    
    let entry = appendBasicBlock func "entry"
    positionBuilderAtEnd bldr entry

    // Make a Metadata node and try and attach it to a ret
    let mdnode = mDStringInContext context "bazquux" 7u
    let ret = buildRet bldr i32zero
    // From http://llvm.org/docs/doxygen/html/classllvm_1_1LLVMContext.html
    // MD_dbg = 0, MD_tbaa = 1, MD_prof = 2, MD_fpmath = 3, MD_range = 4, MD_tbaa_struct = 5
    // Fails here
    setMetadata ret 0u mdnode

    // Save bitcode to file
    writeBitcodeToFile module_ "out.bc"