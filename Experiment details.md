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
* **Requirement normalization**: after every versions, I will come up with some strict requirements that all versions have to follow exactly. So if there are small differences in the details of their behavior, now I change them.
* **MVP**: minimum viable product

### Disclaimer
If by any chance... the "Simple" variation wins... to which I admit I am biased towards... 
That does not mean I learned nothing from TDD, functional, reactive, design patterns, SOLID, etc. It means I LEARNED them, did projects with them, AND therefore i know when and how to use them.

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
(But to keep their own distinct styles, they dont have to use the same text sources!) - this step I'll call requirement normalization.

Eliminate the ones that are too painful to work with. Exceptions can be made for variations that claim to scale better for bigger projects.

#### Then: Repeat these steps for the next Version (V2, V3, etc)!

## Rules: 
* Always try to make the best out of every variation! If something is obviously stupid, dont do it! 
* Use clean code everywhere, simplify everywhere, use (or break) SOLID if needed, etc! - except of corse if that would break the essence of the coding style (Don't skip unit tests if you're doing TDD)
* Don't think ahead of versions! You can't prepare the code for future requirements, so in iteration 1(V1) you only consider V1 requirements. And in V2 only V1 and V2 requirements, etc.
* As an extension of the above, also don't make up future requirements just because you are writing a code that would work great for those requirements. (For example make up an unlikely requirement to replace part of the code, just because you just wrote something that would nicely allow that)

## The test problem:
### First iteration (V1)

For the first iteration, every variation has to create a rock-paper-scissors game, where the player plays against the computer(random), and the first to score 5 wins the game.

So kindof like:

Player: (r)ock, Computer: (s)cissor  
Rock beats scissor! score: 1 - 0  
Player: (r)ock, Computer: (r)ock  
Tie! score: 1 - 0  
Player: (p)aper, Computer: (r)ock  
score: 2 - 0  
...  
Player: (s)cissor, Computer: (r)ock  
score: 2 - 5  
Computer wins!  

But! First the game has to announce the rules!

#### First iteration requirement normalization:

So until this point, every variation could do the MVP any way they wanted. (And in fact I made them all a bit different from eachother on purpose...)  
But at this step, they all have to behave exactly the same way.  
I'll try to always normalize in a way that every version has to change something a little. It should be easy... right?!

First of all, switching between variations should be easy!!

On calling start:  
"Rock, Paper, Scissors!"  
"Game rules:"  
Third, etc...: "You play against the computer. In every round, you hit 'r' for rock, 'p' for paper, and 's' for scissors."  
"Just a reminder:"  
"Rock crushes Scissors,"  
"Scissors cut paper,"  
"And paper covers rock!"  
"The first to 5 wins!"  
"Ready? Go!"  
"3... 2... 1..."

On pressing invalid key: nothing should happen.

On pressing invalid key 3 times after eachother (valid key resets this counter):  
"Valid keys are: 'r' for rock, 'p' for paper, and 's' for scissors!"

On pressing valid key (r for example, and computer does paper):  
"You showed rock! Computer showed paper! - Paper covers rock!"  
"Computer: 1, Player: 0"

On pressing valid key (r for example, and computer does scissors):  
"You showed rock! Computer showed sissors! - Rock crushes scissors!"  
"Computer: 0, Player: 1"

On tie:  
"You showed rock! Computer showed rock! - Tie!"  
"Computer: 0, Player: 0"

Between each round:  
"3... 2... 1..."

But if computer wins:
"Computer wins the match! Better luck next time!"  
If you win:
"You win the match! Congratulations!"  
"Ready for the next match?"  
"3... 2... 1..."

### Second iteration / copy paste all variations and implement V2 requirements(?)
**(Maybe don't include variations that are obviously already bad)**

Alright, i think what i should do is the following: Even if my TDDBetter tests are usable for every variation... 
without having to change anything, or having to plan for it... I should still go ahead without tests for my variations.  
Expect of corse for the TDDBetter, which still should be tested.  
This way i can potentially still prove that without TDD (done right) your code will be bad. (Or not :D)

Extra requirements (but have to do them in order! So "pretend to not know the future" rule!):

1 Different strategies to count (best of n, ties count as win, ties count as .5, need to win by x)  
2 Rock - Paper - Scissor - Lizard - Spock, and other versions! Inject rules?  
3 Logging? Error handling? (sorry wrong key command, or something)  
4 different UIs! (2d GUI and 3d characters?)  
5 Localization?

### Maybe as a final iteration (V3):
**(But only with the most promising variations)**

1 3 or more players mode? - can be CPU CPU or Player Player CPU etc / potentially (fake) online multiplayer?  
2 Tournament? Where groups of players would play against eachother... Maybe on background threads?!  
3 Tokens? - where you could have tokens that give you special powers like double your score, or double both of your scores, or negate other players token, or negate last result, or start with 1 points instead of 0?  
4 Save game/load game?

# Variations (Ideas for variations)
* HelloWorld - example project to test the system that initializes the different variations, and runs them. This one just writes Hello World and thats all. But it also has a test!  
* Simple - gradually evolving, always go towards simplest solution - does not mean it should be dumb  
* TDD - test driven, the way I see almost all companies doing it  
* Data driven programming  
* Functional (using LLMs)  
* maybe a version where i refactor the TDD to a best practice clean architecture something. no need to test first.  
* TDD but better: a unit is not the function or the class, the unit is the module. This idea is from an awesome Ian Cooper talk that i accidentally found during the experiment...  
* Events Variation / Observer pattern/Binding/Reactive - I have decided to give this a shot because at one point it seemed like i could do something interesting.  
For transparency: this style was never close to me, and a little bit I forced this into my experiment. I knew that this may be a bad idea, but my curiosity won.
* ? maybe like a really bad code? :D or something that chatgpt writes? full problem vibecoding in 2024 (chatgpt 4o) - nah i dont wanna waste time
* ? Clean architecture! (Or basically some solution where you decide to use a specific architecture and follow it) - will di maybe in best practice version
* ? maybe best practice? - something that religiously tries to do everything right? (this should not be serious though, more like a caricature and it should be an exception from under the rules) - will do with a combined test with clean architecture
* ? maybe ECS? - meh... performance is not really an issue with this problem and it would be too forced + need to research whats the current best practice, lets not waste time on this.
