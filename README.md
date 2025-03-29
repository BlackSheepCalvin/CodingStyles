# Purpose of this project.

This is an experiment on different coding styles (I call them variations). The same developer (me) will solve the same problem in multiple ways and try to draw meaningful conclusions from the process.

Of course, this is not an ideal scientific experiment, as I serve as both the conductor and the sole participant, without a proper control group. Additionally, I have my own biases and varying skill levels, which may influence the results.

You are welcome to follow along, examine the code at different stages, and form your own judgments.

I am experimenting with:
* a simplistic style as "control", 
* TDD done wrong, 
* TDD done right, 
* functional style (with little functional experience),
* data driven programming, 
* clean architecture (with little clean architecture experience)

## The base problem

The task is to create a rock-paper-scissors game with changing requirements to simulate the unpredictibility of a real development. 
To test this as much as possible, there will be several versions of this task.
Check 'Experiment details.md' to read more about the planned features for versions

* In V1, each coding style variation will solve the Minimal Viable Product(MVP) on their own way. I will ignore requirements from V2 and V3 (to simulate unpredictibility). After each of them is finished, I compare these solutions.
* In V2, I add some requirement changes, and see how the different styles hold up to them. If some style becomes a pain to work with, I eliminate them. I might make exceptions with coding styles that promise to scale well for big projects. (TDD for example)
* In V3, I add even more requirements, and evaluate the remaining styles. (I plan to check flexibility, maintainability, extendability, modularity, reusability, fragility, buggyness, etc, but this may change.)

## Rules: 
* Always try to make the best out of every variation! If something is obviously stupid, dont do it! 
* Use clean code everywhere, simplify everywhere, use (or break) SOLID if needed, etc! - except of corse if that would break the essence of the coding style (Don't skip unit tests if you're doing TDD)
* Don't think ahead of versions! You can't prepare the code for future requirements, so in iteration 1(V1) you only consider V1 requirements. And in V2 only V1 and V2 requirements, etc.
* As an extension of the above, also don't make up future requirements just because you are writing a code that would work great for those requirements. (For example make up an unlikely requirement to replace part of the code, just because you just wrote something that would nicely allow that)

## How to navigate the project:

### More on Experiment details

Check 'Experiment details.md' in main folder

### Read about my observations during development

Check 'ObservationsOutcomes.md' in main folder

### Codebase

Main scene is in RockPaperScissors\Assets

Code is in RockPaperScissors\Assets\CodeBase

### Coding styles / Experiments / Variations
To see the different coding style variations (called variations in the project), go to the '2_UseCases(Variations)' folder inside the CodeBase folder. 

These variations are implemented as part of the UseCases layer. 

Originally I planned to have Clean Architecture as one of the variations, but when I started working on it, I realized something. 
The way I set up the whole project already very closely resembled the Clean Architecture. 
So I decided that I will use clean architecture terminology for the overarching architecture that manages the variations.
And the other variations will just be inside the usecases layer when possible.

Of course I will still create entities and services (in Core and Infrastructure layers) that may not be used by all variations. 
For example the JSONReader service initially will only be used by the Data Driven Variation - this is how it will read its data.

### CodeBase folder / Clean Architecture explanation

For reference, here is Uncle Bob's explanation on clean architecture in general: https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html

CodeBase folder has 4 subfolders in it, these are the different layers of the Clean Architecture. They all have their own assembly definition file too.

Unfortunately in my opinion there are way too many buzzwords for architectural patterns, even just in the context of Clean Architecture. 

I tried to choose names that would make sense in the project's perspective, but also would sound similar to most developers. 

This is why some of my folder names are made up from multiple words.

##### 1_CoreAndUtilies
This folder contains everything that is common. Utilities, common business rules and entities, common interfaces. 
Some developers would maybe split this into two or even more modules, but for me, 4 main modules are enough, and I don't expect Core to grow too big in the project.
Others could call this module Entities, Enterprise Business Rules, Core or Utilities. Also I know people who'd put some of these business rules and entities into Domain. 
But the name "domain" would better describe the next module.

##### 2_UseCases(Variations)
This folder contains most implementation details and logic, and it contains the essence of the experiment itself.
Each Variation has their own folder inside, with their own assembly definition file. They **depend** on CoreUtilities module and nothing else.
Alternate names for this module could be: Application Business Rules, Domain, Usecases, Model(?)

##### 3_Presentation
This folder contains everything that is needed to run the Variations in Unity. 
For example to present some feedback to the user about what is happening, allow the user to interact with these systems through UI buttons, keyboard presses, etc.
It **depends** on CoreUtilities and UseCases.

##### 4_Infrastructure
This folder is about specific implementations for services like: random generator, debugger, and I/O (JSONReader). It depends on interfaces defined mostly in CoreAndUtilities. 
It also contains the 'Initializer' MonoBehaviour, which assigns these dependencies, sets up the UI, and starts the main flow.
It can **depend** on any other modules