## Environment
Optional block that must be first in a program

Environment details are encased in a special block, starting with `#env` and terminating with `#envstop`. Can be on a single line and
flags don't need to be separated by anything

```
#env
 ... any environment string flags to be written here ...
 ... all other content is ignored ...
#envstop
```


#### `disallow-pseudo-instructions`
Will not decompose pseudo instructions, causing the parser to not recognize them.
Can cause a side effect of parsing not ignoring unrecognized instructions

#### `disallow-user-specials`
Will change all special instructions from the user (not from a decomposed pseudo instruction) to turn into
a `noop`

#### `strict-parse`
Will force parsing to stop and execute *nothing* if a unrecognized instruction is read.
Overwrites default behaviour of ignoring unrecognized instructions

#### `enable-debug`
Unimplemented as of writing

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

#### CopyFromL `cpyfl`, `c`
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

#### PeekJump `pkjmp`, `$`
Reads value at `mp` and sets `mp` to read value

### Instruction Pointer Basic Instructions

#### Skip `skip`, `.`
Unconditionally skips the next instruction

#### Restart `rstrt`, `@`
Unconditionally moves instruction pointer to the first instruction

#### Terminate `halt`, `!`
Completely stops the program

---

## Special Instructions
These are basic instructions that are mainly used for pseudo instructions, they aren't restricted by default though (with some caveats in some instances)

#### ReplaceToSpecial `rtspl`
Copys value at `mp` to memory location 0xFF

#### ReplaceFromSpecial `rfspl`
Copys value at memory location 0xFF to `mp`

#### MemPointerToSpecial `mpspl`
Copys `mp` to memory location 0xFF

#### JumpSpecial\* `jpspl`
Moves `mp` to memory location 0xFF

---

## Non-basic Instructions
These instructions use arguments and **cannot** be used in another instruction for conditionals or grouped

#### OnZero `whenz`, `z`
Checks value at `mp` if it's zero. Runs the basic instruction given if check completes

Usage: `whenz <basic-instruction>`

#### InTopHalf `whnth`, `t`
Checks if `mp` is in the top half of memory (`mp` < 0x80). Runs the basic instruction given if check completes

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

## Footnotes

\* Note when not replaced within a pseudo instruction these are by themselves a pseudo instruction, so when you use them they decompose into multiple but when a pseudo instruction does they do *not* decompose

## Other
Pseudo instruction reference [here](PSEUDO_REF.md).