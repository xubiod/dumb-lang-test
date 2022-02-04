# dumb-lang-test
A quick attempt to make an esolang and C# parser.

## Design Decisions
- All instructions are made with both broad and narrow use cases in mind
- All instruction names in longer form are 5 characters or less
- All basic instructions have no given arguments
- All non-basic instructions cannot be used in other non-basic instructions
- Memory is by default an array of 256 items consisting of C# `byte` primitives
  - Memory must wrap around the memory pointer overflows
  - Full memory map is printed when a program finishes or is forced to stop
    - Byte guides on axis
- All instructions can only modify values at the memory pointer
- All instructions can read values around the memory pointer
- The memory pointer can jump to any point in the memory space with a single instruction
- Loops are infinite, only breakable using a conditional with the `skip` instruction


Instruction reference [here](REFERENCE.md), pseudo instruction reference [here](PSEUDO_REF.md).