#### `andl`
Performs `[value @ mp] = [value @ mp - 1] & [value @ mp]`

Decomposes into `shftl; rtspl; andr; shftr; cpyfl; shftl; rfspl; shftr`, overwrites location 0xFF

#### `orl`
Performs `[value @ mp] = [value @ mp - 1] | [value @ mp]`

Decomposes into `shftl; rtspl; orr; shftr; cpyfl; shftl; rfspl; shftr`, overwrites location 0xFF

#### `xorl`
Performs `[value @ mp] = [value @ mp - 1] ^ [value @ mp]`

Decomposes into `shftl; rtspl; xorr; shftr; cpyfl; shftl; rfspl; shftr`, overwrites location 0xFF

#### `swapr`
Swaps value at `mp` and value at `mp + 1` with each other

Decomposes into `shftr; rtspl; cpyfl; shftl; rfspl`, overwrites location 0xFF