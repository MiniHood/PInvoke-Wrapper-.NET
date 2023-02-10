# PInvoke-Wrapper-.NET
Easier PInvoke for CPP dlls.

## Example
```int result = PinvokeWrapper.CallCppFunction<int>("mycpplibrary.dll", "CppFunction", 10, 20);```
