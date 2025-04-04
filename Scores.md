## Scores
These are obviously super subjective. They are based on the actual codebase I have so far, but my opinion may be influenced by past experience too, here and there.

**Dont take this part too seriously!**

I dont know what i want to do with this section, this is so subjective, probably noone cares. But for me its fun and i am interested to see how this evolves over time.

I'm gonna keep updating this as i go along, and i can check back in version control probably after V3

### Maintenance/extendability:

| Style                          | Rating    | Notes  |
|--------------------------------|-----------|------------------------------------------------------------------|
| **Simple one**                 | **10/10** | Used as a basis for many other styles; easiest to change, extend, and understand. |
| **TDD (done wrong)**           | **1/10**  | Mocks, interfaces, coupled tests to implementation—it's a nightmare. Never want to touch it again. |
| **Data Driven Programming**    | **8/10**  | New requirements always make you think: "Great... how do I make this generic?" |
| **Functional**                 | **6/10**  | Hard to change; you have to re-learn the part you're working on every time. |
| **Clean architecture**         | **10/10** | Evolved naturally from the experiment’s architecture. Couldn't keep it as a separate variation — it merged into everything. After realizing this, I named modules following clean architecture conventions. |
| **TDDBetter (TDD done right)** | **10/10** | Tests were reusable across all variations without modification. Very impressive. |

Events Variation: (I decided not to rate this as it wouldn't be fair to it)

### Readability:

| Style                          | Rating    | Notes  |
|--------------------------------|-----------|------------------------------------------------------------------|
| **Simple one**                 | **10/10** | Simplicity means easy to write, easy to read, easy to extend. So readability is high as expected. |
| **TDD (done wrong)**           | **8/10**  | One reason why this scored high is because TDD done wrong usually leads to a very uniform codebase for the wrong reasons. (As it did with my code too). Architecture the same, the way you test is the same, where you put your private vs public methods, the way you use your properties, the mocks, everything is the same because otherwise it would be harder to navigate and make sense of the code. And also it would take forever to develop and write tests too in a sensible amount of time. |
| **Data Driven Programming**    | **6/10**  | This is ambivalent. In one sense DDP readability tends to be really high, because there are no big surprises/tricky solutions in the codebase, it just does what the data says. But to make some feature work as a generic data driven thing... sometimes things are not as straightforward still. |
| **Functional**                 | **3/10**  | Well i think this is no surprise and a well known critique of the functional style. It can get crazy. |
| **Clean architecture**         | **N/A**   | Or i should say clean architecture done right, should not affect readability much. It should just be obvious that infrasturcture is abstracted out to the outer layer. Business logic is in usecases, and the most common stuff thats used everywhere is in core. No magic, no trends to follow. Just naming conventions so we speak the same language. |
| **TDDBetter (TDD done right)** | **N/A**   | TDD done right allows you to use any kind of architecture/style, and allows you to experiment, so it makes no sense to rate readability. |

### Ease of Use:

| Style                          | Rating    | Notes  |
|--------------------------------|-----------|------------------------------------------------------------------|
| **Simple one**                 | **10/10** | Who could have anticipated this? If something is simple, then it is easy to use... |
| **TDD (done wrong)**           | **6/10**  | TDD done wrong is freighteningly easy to learn. But of corse you have to understand DI. Also after a while, helper methods, automating mocking, and a bunch of other best practices for tests emerge. |
| **Data Driven Programming**    | **4/10**  | When you have a requirement that goes against your existing data structure, it can be hard to keep things generic, and hard to decide what to drive by data, and what to hardcode. |
| **Functional**                 | **3/10**  | It hard to learn all the tricks, and also its hard to memorize what functions are available to you and how they work. Also what about states? (Note that on this project there is no multithreading yet where functional could shine in this category though. ) |
| **Clean architecture**         | **N/A**   | Once it is in place, it doesn't really effect the code or your coding experience. |
| **TDDBetter (TDD done right)** | **5/10**  | What is a unit? What classes can you test together? What should you test exactly? How should you test it? - These are all simple questions if you do it wrong. But if you do it right, they become a bit harder. But then again... it makes it a lot easier (safer and faster) to refactor your code! |

### Funfactor:

| Style                          | Rating    | Notes  |
|--------------------------------|-----------|------------------------------------------------------------------|
| **Simple one**                 | **5/10**  | I'd say its average. Booring predictible code, but you can also progress fast. |
| **TDD (done wrong)**           | **2/10**  | Repeating the same thing again and again is not fun. Rarely you can optimize something, then it is a little. |
| **Data Driven Programming**    | **8/10**  | When you think of all the possible ways your app can be changed just by changing the data... is awesome. Especially if you do have to extend it, and you can do it just with data. Or when you have an idea: what if this thing had this feature too? And then you just flip it on, and now it does! |
| **Functional**                 | **10/10** | Thats why its called FUNctional, no? :) Anyway its great to try something crazy, and realize that its actually super safe, because you have no side effects. |
| **Clean architecture**         | **N/A**   | N/A once again. |
| **TDDBetter (TDD done right)** | **4/10**  | Testing is not fun. But done right its not the end of the world. And actualy there are times when you enjoy it, especially when you find some optimization, or when it saves you, or when you have a great idea on how to test something complex, in a simple way! |

### Good Vibes (objective counting of my subjective thoughts and feelings :D):

| Style                          | Rating    | Notes  |
|--------------------------------|-----------|------------------------------------------------------------------|
| **Simple one**                 | **6** |  |
| **TDD (done wrong)**           | **3**  |  |
| **Data Driven Programming**    | **8**  |  |
| **Functional**                 | **6**  |  |
| **Clean architecture**         | **1** |  |
| **TDDBetter (TDD done right)** | **6** |  |

### Bad Vibes:

| Style                          | Rating    | Notes  |
|--------------------------------|-----------|------------------------------------------------------------------|
| **Simple one**                 | **2** |  |
| **TDD (done wrong)**           | **8**  |  |
| **Data Driven Programming**    | **3**  |  |
| **Functional**                 | **6**  |  |
| **Clean architecture**         | **1** |  |
| **TDDBetter (TDD done right)** | **2** |  |

### Beauty:

| Style                          | Rating    | Notes  |
|--------------------------------|-----------|------------------------------------------------------------------|
| **Simple one**                 | **5/10**  | basic |
| **TDD (done wrong)**           | **3/10**  | a bunch of interfaces, a lot of tests that test implementation, dependency lists |
| **Data Driven Programming**    | **8/10**  |  |
| **Functional**                 | **10/10** |  |
| **Clean architecture**         | **N/A**   |  |
| **TDDBetter (TDD done right)** | **5/10**  |  |