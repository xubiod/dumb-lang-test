## Basic Instructions
Basic instructions contain no arguments

#### BumpUp `bumpu`, `k`
Performs `[value @ mp] = [value @ mp] + 1`

#### BumpDown `bumpd`, `j`
Appears to perform `[value @ mp] = [value @ mp] - 1`

As it works with bytes, it actually performs `[value @ mp] = [value @ mp] + 0xFF` in C# code

#### Noop `noop`, `-`
Does nothing, spends a cycle

#### Read `read`, `i`
Reads a key from standard input. Sets value at `mp` to key

#### WriteRandom `randm`, `?`
Sets value at `mp` to a random number from 0x00 to 0xFF

#### WritePointer `wrptr`, `v`
Sets value at `mp` to `mp`

#### CopyFromL `cpyfl`
Sets value of `mp` to value 1 left of `mp`

#### BitWiseXorR `xorr`, `^`
Performs `[value @ mp] = [value @ mp] ^ [value @ mp + 1]`

#### BitWiseAndR `andr`, `&`
Performs `[value @ mp] = [value @ mp] & [value @ mp + 1]`

#### BitWiseOrR `orr`, `|`
Performs `[value @ mp] = [value @ mp] | [value @ mp + 1]`

#### BitWiseCompliment `compl`, `~`
Performs `[value @ mp] = ~[value @ mp]`

#### Parity `prity`, `%`
Checks parity of value at `mp`.
If odd, value at `mp` is set to 0xFF.
If even, value at `mp` is set to 0x00

### Pointer Instructions
Note these are considered basic instructions

#### Reset `reset`, `0`
Sets `mp` to 0

#### ShiftL `shftl`, `<`
Moves `mp` over to the element on the left. Will wrap around to 0xFF if `mp` was at zero

#### ShiftR `shftr`, `>`
Moves `mp` over to the element on the right. Will wrap around to zero if `mp` was at 0xFF

#### PeekJump `pkjmp`
Reads value at `mp` and sets `mp` to read value

### Instruction Pointer Basic Instructions

#### Skip `skip`, `.`
Unconditionally skips the next instruction

#### Restart `rstrt`, `@`
Unconditionally moves instruction pointer to the first instruction

#### Terminate `halt`, `!`
Completely stops the program

---

## Non-basic Instructions
These instructions use arguments and **cannot** be used in another instruction for conditionals or grouped

#### OnZero `whenz`, `z`
Checks value at `mp` if it's zero. Runs the basic instruction given if check completes

Usage: `whenz <basic-instruction>`

#### InTopHalf `whnth`, `t`
Checks if `mp` is in the top half of memory (`mp` > 0x80). Runs the basic instruction given if check completes

Usage: `whnth <basic-instruction>`

#### Addi `addi`
Adds all given immediate bytes together with `mp` and sets value at `mp` to result.

Usage: `addi <byte>,<byte>,...`

#### Group `group`, `g`
Groups together two or three instructions to one line, at the cost of using 3 cycles per instruction in group, with group taking its own cycle

Skip will skip all instructions in the group

Usage: `group <basic-instruction>,<basic-instruction>[,<basic-instruction>]`

#### JumpOffsetFine `jmpof`
Moves instruction pointer by a given offset. Uses a **signed** byte

Usage: `jmpof <sbyte>`

#### JumpOffsetCoarse `jmpoc`
Moves instruction pointer by a given offset multiplied by 128. Uses a **signed** byte

Usage: `jmpoc <sbyte>`