# Overview

This project is about comparing different programming techniques, 
* see how they hold up against changes, 
* how easy to work with them, extend them,
* how nice is the code they produce, 
* how easy is it to understand them
* how safe or buggy they become
* how easy was it to make it? 

Note: Time it takes to do an iteration is not really measurable, because: 
* I am doing this in my free time, so I cannot be consistent with every variation, 
* I am also more experienced in some styles than others. - ECS reactive and functional I am not super comfortable
* The order i do these projects in affects the time it takes to do them. Because i get better at solving the same problem + i reuse code a lot. (Even more than normally since the task is the same for all approaches!)

### Terminology

* **Versions**: (V1, V2 and V3) - to simulate real world requirements, first we have to write code to satisfy Version 1 (V1) requirements, and we must "pretend" we dont know about V2 and V3. Then we do V2, and see how extendable and maintainable the code is.
* **Variation**: the implementation of a particular style of programming for the test problem
* **Test problem**: the test every Variation has to solve. Its basically a Rock-Paper-Scissors game.

### Disclaimer
If by any chance... the "Simple" variation wins... to which I admit I am biased towards... 
That does not mean I learned nothing from TDD, functional, reactive, design patterns, SOLID, etc. It means I LEARNED them, AND therefore i know when and how to use them.

### Plan of things to do
#### Create the variations for the first version (V1)
#### When every variation of a version is done:

Commit to git repo.

All variations should be tested by change request ideas like:
* could you change this or that text?
* could you change this or that behavior?
* what if we want to introduce something else?
* what if we change the rules?
* what if we want to write something out differently?
* what if we want to log something? Kindof like analytics?

Initially variations can differ from eachother a little, but during this testing phase, all variations should be changed so the write exactly the same messages and behave the same way. 
(But to keep their own distinct styles, they dont have to use the same text sources!)

Eliminate the ones that are too painful to work with. Exceptions can be made for variations that claim to scale better for bigger projects.

#### Then: Repeat these steps for the next Version (V2, V3, etc)!

## Rules: 
* Always try to make the best out of every variation! If something is obviously stupid, dont do it! 
* Use clean code everywhere, simplify everywhere, use (or break) SOLID if needed, etc! - except of corse if that would break the essence of the coding style (Don't skip unit tests if you're doing TDD)
* Don't think ahead of versions! You can't prepare the code for future requirements, so in iteration 1(V1) you only consider V1 requirements. And in V2 only V1 and V2 requirements, etc.
* As an extension of the above, also don't make up future requirements just because you are writing a code that would work great for those requirements. (For example make up an unlikely requirement to replace part of the code, just because you just wrote something that would nicely allow that)

## The test problem:
#### First iteration (V1)

For the first iteration, every variation has to create a rock-paper-scissors game, where the player plays against the computer(random), and the first to score 5 wins the game.

So kindof like:

* Player: (r)ock, Computer: (s)cissor
* Rock beats scissor! score: 1 - 0
* Player: (r)ock, Computer: (r)ock
* Tie! score: 1 - 0
* Player: (p)aper, Computer: (r)ock
* score: 2 - 0

...

* Player: (s)cissor, Computer: (r)ock
* score: 2 - 5
* Computer wins!

But! First the game has to announce the rules!

# Variations (Ideas for variations)
* HelloWorld - example project to test the system that initializes the different variations, and runs them. This one just writes Hello World and thats all. But it also has a test!

* Simple - gradually evolving, always go towards simplest solution - does not mean it should be dumb

* TDD - test driven, duh...

* Data driven programming

* Functional (using LLMs)

* maybe a version where i refactor the TDD to a best practice clean architecture something. no need to test first.

* ? Observer pattern/Events/Binding/Reactive? - i dont know what fits best. maybe ask AI? - Focusing on "decoupling" (is it really decoupled)? - maybe not because 
i'd just try to prove a point that events can be dangerous, and people would say i use them "incorrectly", but if i use them "correctly" then its not much difference (for this task) than the other variations.
Just a note that observer pattern, and reactive programming CAN be very useful, depending on the problem. My rule of thumb is that reactive programming can be useful if you are explaining a system like: 
"if this happens then the app should react like this or that, those things shuold also change, blabla. Or this this and this field here should always show the latest data that we get from here or there..."
Long story short: if i dont anticipate anything interesting from a coding style, then why waste my time with it? 
(OFC if i really wanna learn about reactive (and its always a good idea to learn something new), then trying to solve a problem with reactive (especially something that is promising to be interesting), - is not a waste of time)
BUT! maybe i can do a little reactive in the best practice clean architecture!

* ? maybe like a really bad code? :D or something that chatgpt writes? full problem vibecoding in 2024 (chatgpt 4o) - nah i dont wanna waste time

* ? Clean architecture! (Or basically some solution where you decide to use a specific architecture and follow it) - will di maybe in best practice version

* ? maybe best practice? - something that religiously tries to do everything right? (this should not be serious though, more like a caricature and it should be an exception from under the rules) - will do with a combined test with clean architecture

* ? maybe ECS? - meh... performance is not really an issue with this problem and it would be too forced + need to research whats the current best practice, lets not waste time on this.

## Second iteration / copy paste all variations and implement V2 requirements(?)
**(Maybe dont include variations that are obviously already bad)**

Extra requirements:

* Different strategies to count (best of n, ties count as win, ties count as .5, need to win by x)
* Rock - Paper - Scissor - Lizard - Spock, and other versions! Inject rules?
* Logging? Error handling? (sorry wrong key command, or something)
* different UIs! (2d GUI and 3d characters?)
* Localization?

## Maybe as a final iteration (V3):
**(But only with the most promising variations)**

* 3 or more players mode? - can be CPU CPU or Player Player CPU etc / potentially (fake) online multiplayer?
* Tournament? Where groups of players would play against eachother... Maybe on background threads?!
* Tokens? - where you could have tokens that give you special powers like double your score, or double both of your scores, or negate other players token, or negate last result, or start with 1 points instead of 0?
* Save game/load game?
