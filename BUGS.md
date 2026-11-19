# BUGS

The starter contains behaviors that contradict `ASSIGNMENT.md`. Write here every discrepancy you found.
Fill it in yourself, in your own words. There is no required number: what counts is that they are real.

| Which rule from ASSIGNMENT.md is violated (R1–R8) | Where the problem was in the starter (file, approximate line) | How you discovered it (test, reading, AI) | What you changed |
|---|---|---|---|
| _R6_ | `Services/BookingService.cs`, in the `Create` method | _Picked a simple looking rule and went and checked the files myself_ | _Added a check using `endUtc` and `startUtc` to make sure it's >4 hours_ |
