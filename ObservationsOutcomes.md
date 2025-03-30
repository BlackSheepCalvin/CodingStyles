# Observations / Outcomes

Here I document everything every time i am annoyed or impressed. I do this immediately, as i code, and i don't write down anything else. So none of these thuoghts are fabricated, or past experiences, etc...

\+ means I am impressed  
\- means I am annoyed  
\* means I am not sure or just had a thought

## Observations:

### Simple:
#### V1:

\- The first bigger negative for the Simple solution was that after some refactoring that i did for TDD in the Core module, i wanted to see if everything works well still... but running the tests was not enough anymore! So just for the simple solution, i had to run and manually smoke test the game to make sure it still works

\+ Obviously it was very easy and quick to work with this

#### V1 normalization:

\+ Although this variation was never planned to be tested, when i created the implementation independent tests for TDDBetter, I could actually run those exact tests on the Simple variation...  
What is really interesting to me, is that i thought my code was not testable. Because my classes had dependencies on other classes directly, and not on interfaces. But it was, in fact, **testable**...

\+ Super easy to normalize, fast, reliable. And i even had an idea to refactor. Refactoring was also super easy.  
I spent the majority of the time on writing the TDDBetter tests, and applying the refactoring on other solutions.  
TDD, TDDBetter and Data driven was the hardest to refactor for the new idea, but wasn't too bad, and it was kindof expected, so i wont deduct points from them.

\+ After running the TDDBetter unit tests to check if my final solution works or not, i actually had to make more fixes in the tests, than in my code... :D

\+ I've decided to try the event(/observer) variation. Guess which variation I choose as a starting point for the event one? - The Simple. Y'know... 'cause its **simple**.

\* I'm not even gonna add extra points now so i don't do favoritism but I've copied code from Simple one to TDDBetter when normalizing TDDBetter.

### TDD done wrong. Exactly how most big companies (that i've worked for) enforce it:
#### V1:
\- TDD was obviously tedious to start, and kindof numbed my brain with the repeated test writings when it came to actually having to come up with an idea on how to implement the game. 
(In real life on longer projects this obviously wouldn't be a big issue because you get used to writing tests)

\- Also it (TDD) made me write a bunch of code that i had to refactor later - of corse this could be mitigated if you do it a bit smarter, but that means you'd think of architecture first and THEN tests. So NOT test first, not TDD.

\- Also with TDD I did manage introduce a small bug earlier than I expected... I wrote a bad test and then a bad code. This can happen the other way around too, people write bad code, and then "fix" the tests. I am just writing this because often people have the false impression that unit tests protect you against all bugs.

\+ Very nice that almost feels like i dont even have to manually test this variation after changing the core codebase.

\* Refactoring wasnt that bad, some tests could be salvaged

\- I did pretty soon get a lasagna code feeling where i was overwhelmed by the number of places i had to change things to do a refactoring / extend the code.

\* Just a note that if you are experimenting, you shouldnt continously rewrite code and tests. Write tests once, then experiment, then if you feel you are done, fix the tests. Although i am not sure if you know ahead of time that you are about to do experimenting and not actual coding.

\* An other note: it is also quite known that TDD is best if you have a concrete task that is quite complex, but you know exactly what the required behavior should be.

\+ Ok to be fair, i will mention this, maybe its duplicate but... after i finished writing all tests for V1, and they were all green, i tested the game manually for the second time throughout the whole development, and it worked perfectly!

\- My brain feels like sh*t after i finished this though...

#### V1 normalization:

\- Slightly annoying but when i moved the string constants to core from the individual Variations, only TDD and Simple variants needed adjustment in code...  
But only for TDD, it also needed adjustments in the unit tests.

\- ... aand that wasnt the only thing that got broken. An other test also got broken, because rock crushes scissors, not smashes!! Soo important.
i'm not giving an other minus but i had to re-run tests one more time: this time because of an exclamation mark in the string - that i did not notice before!

\- I have decided that i am not going to kick the dead horse any longer. Yes, TDD is "supposed" to perform better on big, and complex projects, so i should give it a chance...  
But to me at least it is proven without a doubt, that (if I'm doing it) TDD **done wrong**, is bad, and it gets much worse over time. - Which aligns perfectly with my experience on big projects that enforced it.  
**ELIMINATED**

### Notes regarding TDD and Simple:

\- I did introduce a bug with the Simple solution because i broke liskov substitution principle for enums... i added an invalid case to the Signs enum. But now that i think of it... i am not sure TDD would have prevented this bug!

\+ TDD was actually reusing a lot of code from the simple one, and it was very easy to reuse (obviously mostly because we had the same task :D In real life this would never happen, whoever wants to reuse, will want to use the module differently) 

### Data Driven Development:

\+ LLMs (chatgpt 4o) are great for assisting with jsons and structs

\+ I was able to focus on architecture again (after doing the TDD version), finally, and now i have a really good idea of what i want, and i could do it fast

\+ Obviously this is gonna be really easy to tweak, and looks awesome, for the first time everything is dynamic. (rock paper scissors, and the rules)

\+ Rule generation is by default reusing everything, no repetitions

\+ For the first time i recognized i need to create a keyPress interpreter that would work with signs that begins with different letters (scissors, spock - for example)  
I realized this because rock paper scissors is now dynamic, so i was thinking of how to handle key presses for any input data

\+ I did a TDD-like approach only for the interpreter which is actually complicated to do, so TDD actually helped me, rather than slow me down. But i decided that this interpreter goes straight to Core.

\- There was a relatively hard to fix bug with this line (Rule is a stuct so its default is not null...):  
`Rule? playerWinsRule = gameData.rules.FirstOrDefault(rule => rule.winner == playerSign && rule.loser == computerSign);`  
This complexity was not needed yet, but it will be necessary for every versions later!  
Note that I immediately found the bug, and as i said, it would have happened in any other versions too.

\+ When i did Clean Architecture, I had to replace JSONReader with an interface, but it was super easy to do! (I guess this will be needed for TDD as well)
It was literally just: 
* creating a new interface
* making JSONReader confirm to that interface
* rename the method signatures, types, and names (because i changed all these for the interface to be more generic and customizable)
* moving the JSONReader from Core to Infrastructure
* Then it worked perfectly for the first time!

### Functional:

\+ Fun to do :D

\+ Super compact end result

\+ High cohesion... for now. Nice that a lot of important stuff can be read one after the other.  
One function tell you who wins, function below that tells you what to do when someone wins, function below that tells you if game ended or not.

\+ Easy to test, and i used LLM to write tests (the LLM needed a little help but then it was great!)

\- Sometimes very hard to read, and i am (initially at least) tempted to write less readable code because its more fun.

\* Just a note that I dont think it would scale well, but we'll see

\* to be honest V1 does the bare minimum for now, since the LLM went the easiest routes. But! Its good because we'll see how it scales!

\+ i dont measure time but i finished this quickly in one night, after work+playing eve online, next morning i wrote unit tests before i went to work. (And played eve :D)

#### V1 normalization:

\- Yes functional was easy to test, but it was easy to test the FUNCTIONS. I tested the wrong thing, and now i am deleting those tests, they are useless and it would be a burden to update them.  
In other words: if you are doing testing right, functional may not be easier to test than any other style. Because your tests should be independent of implementation!

\- Allright... it started great, then it was harder, then it was a little fun, but now... i'd just say this is just very annoying :D I have to keep changing my functions, their signatures,  
and i have to keep coming up with hacks about how to change this thing to work the way i want.  
All while keeping all the requirements in my head, and also every detail of how this beautiful mess works. This was much easier with every other style.

\- Note that i dont have to write tests - OMG btw the tests... if rewriting the signatures of all these functions is annoying, imagine how annoying would be to rewrite all the tests too...  
The list for the previous point started to grow too long, so i split this into two.

\- Everything got more tricky, and things are not in one place anymore. So cohesion suffered.  
Probably when the first time i wrote the code it did not have strict requirements so i went for the most convenient solutions.

\- I've encountered a line with a comment "// bruh...". So probably it already looked a bit tricky when i wrote that line.  
I think I don't have to tell you that i have no idea what is going on in that line :D

\+ Still a bit fun, in a puzzle-like way. I am not sure this is a positive, but i didnt want to be so negative :D

### Clean Architecture (/following a specific architectural design pattern):

\- Immediately i felt like i have to create a bunch of assemblies, classes and interfaces, that make no sense at all, and i couldn't imagine a scenario where they'd be useful. 
Luckily i had this rule at the beginning that i wont do stupid things, so i did not start manifacturing a bunch of interfaces and pointless classes. But an inexperienced developer may do. 
And i've seen this happen on multiple projects.

\* Weeelll this is interesting. I gave some more thought of this architecture, and it turns out i can't create the clean architecture version because i already had a clean architecture in place, almost. 
* The initializer and the monobehavior classes that lets you choose between the different variations are basically the presentation layer. 
* The different variations are the usecases
* And I already had a core for the common entities, dependency interfaces, and other common stuff

\* The only thing that clean added to my project is that I moved the Dependencies (or "infrastructure") outside. Like the Random generator, and the Jsonreader. 
(The IPrinter already had an interface that i created for the Simple Variation, because it made sense immediately...)
But i am not sure that i'd ever replace these, or that they would be suuper hard to replace. I imagine replacing one of these on a reaaaly big project would take like... a full day.
Is it really worth worrying about, and maintaining/enforcing something that would take a day to replace?
On different projects maybe... but on my project i cant imagine thinking: oh, lets replace system.random with unityengine.random, its sooo much better!
Or: hmm, we should get game data that we had in a json... from a website! Or from an xml! From a local sql database! - why would I do that?

\* I was thinking to add this requirement for V2 or V3 to change the jsonreader or random generator, but the rules were that i shouldnt know about future change requests... 
And i did not think about these requests because they make no sense...

\+ I am adding this line here from the future in defence for Clean. Creating the infrastructure layer actually made a lot of sense with my discovery about TDD:  
*the units in unit tests are not the classes*.  
This led to me not having to mock everything. Guess what are the only things that still needed mocks: the classes in the infrastructure layer!

### TDDBetter (write tests for modules, not for classes!)

\- Tests are still booring

\+ There is a hope that maybe, I'm thinking I could reuse the tests I write here, for my other Variations?  
If the tests are not depending on internal implementation, this could be done, right? It would actually be impressive.
(Idea: Test the Variation interface itself, and inject the concrete implementations into the test!)

\- Still annoying to rewrite tests as I change requirements.

\+ Honestly in the end things got a bit complicated and confusing, but when my tests were finally green, I tested manually, and it was perfect.

#### V1 normalization:

\+ This is super interesting to me. The same tests, that i wrote for TDDBetter variation... turns out i can reuse them for all the other implementations! Without ever planning for this.

\+ I can now update the tests for the normalization, and use tests written for this one variation to verify all other variations, if they work the way they are supposed to, or not... very nice.

\+ This actually deserves an extra point (I think its quite obvious anyways, that this "experiment" is highly subjective). TDD done wrong -> fragile tests. TDD done right -> suddenly everything is tested.

\+ TDDBetter indeed proves very useful for quicky verifying the normalization of all other variations. Also it makes all the tests that i wrote for those, obsolete...  
more code is ALWAYS a code smell. This applies to unit tests as well. And less code means you're on the right track!

### Observer pattern / Events / Binding / Reactive etc...

\* I just wanna start by saying that Observer pattern and its different forms are perfectly acceptable patterns, and a lot of programmers like them.  
I am not one of them, but i am also not saying that it is bad to use these. So this is not an attack against any of these patterns.  
Also to defend observer pattern in this experiment: Observer pattern have a very specific role, it never claims to be a solution for everything.  
There are times when i am also using observer pattern, and it works perfectly.  
I forced observer pattern into this experiment.

\+ Looked like a good idea to just gather together all the things that can happen, and write the reaction to those events. Kinda like how a unit test writes down what should happen to certain inputs.

\* Immediately a lot of code smells. Of corse only time will tell if those code smells are valid or not.

\- Time immediately told. The code smells are valid.  
If i have a new requirement i have to create a new event, and i have to implement logic for that event two places, while hiding the fact / pretending that those things are related.
I am not even sure how to create these events because based on requirements they can turn out to be wrong. I decided that the safest way is to add a Sign to the event's parameters, 
which means i am now coupling things to these events, so that i can do logic with them in both places.

\- I immediately regretted my decision to do this approach, and i am just writing complaining comments everywhere in the code :D

\- Well thats a hot pile of sh*t. I am sorry if I did it incorrectly, i honestly did my best to make the best out of it, and for a few minutes,  
i really thought it would work relatively well here. (But i was sure it would fail later)

\* I'm not gonna mark it as a negative but FYI my first run after i implemented this, failed.  
It was based on the already working simple solution, so its quite an achievement. But i guess one failure after a refactoring is ok.

\* For the reasons i stated in my first point, i think i am not going to include observer pattern to the final statistics.

\* Allright, i am not going to prolong the inevitable. **ELIMINATED**

## Order of implementation:
Unfortunately i cannot start a new variation with a fully clean brain, also since i choose to do this in one project i can reuse code from time to time. But it is actually interesting to see how easy it is to reuse code from one solution to the other.

Anyway, here is the order in which i implemented these variations:

1 Hello world (Just to test the initialization, and unit test setup)

2 Simple one

3 TDD (**ELIMINATED** in V1 normalization)

4 Data Driven Programming

5 Functional

6 Clean architecture (but this is basically just a restructuring of the project + discovering that I almost did a perfect clean architecture without even realizing it)

7 TDDBetter (TDD but writing tests for modules, not classes...)

8 Events Variation (**ELIMINATED** in V1 normalization)

------------------------------
Future plan:

V2:

1 Functional

+1 MVVM or MVP?! + No magic numbers?! - in the presentation layer!!! for the different UIs?

2 Data driven Programming

3 TDDBetter

4 Simple one

V3 (not sure yet):

1 Simple one

2 Data Driven

3 Functional

4 TDDBetter