# Abstracion based pattern recognition

This approach is based on learning the learning.

This approach uses a base operation, like addition over {0, 1}, where the domain is equal to the codomain
and expresses the differences between the subsequent elements of a pattern

for example:

pattern: 1, 0, 0
differences: 1, 0

then name an **abstract** operation, which looks at the **differences of the differences**.

pattern: 1, 0
differences: 1

we can see that the pattern shrinks to 1 element, which can tell if the whole pattern changed or not

**without abstraction** we would only be able to check if the patterns last element changed from its previous, which is a much less wholistical approach

this step seems trivial at this level using collections and loops or recursive calling,
where the order of indecies tell what differentiates what.

But if we want to abstract even further, where we differentiate 2 of our base systems, so one can look at the other
we can no longer rely on basic indecies to tell what differentiates what, because we have **2 types of differentiation**

So we need to **name** explicitly our operations.

For example, if the base system had the name of 1
we differentiate a name using some divider, like a ".", so we get 1.1

so 1.1 looks at 1
1.1.1 looks at 1.1

and with that, we made a new differentiating system.

if we would want to abstract even further, we could name our dividing elements, creating a new differentiating system.
but then we would need a convection that you read the dividers name first, then the actual name.

if we would want to differentiate different name readings, we could use the same system for that as well.

**the good news is that** these super abstract operation only appear when the pattern is really long, and even then, only for 1 step, and you would need the square of that length to make the super abstract stuff to be long enough to have an another super abstract operation on top of it, which would require an even more abstract operation that can compare them.

but we can see that there is no final solution to this, because as soon as we define a program like that, we can use that same program to differentiate itself, making a new program.
