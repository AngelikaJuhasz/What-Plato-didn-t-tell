VAR correct = 0
VAR incorrect = 0
VAR humorous = 0

-> Start 

 === function lower(ref x)
 	~ x = x - 1

 === function raise(ref x)
 	~ x = x + 1


=== Start ===
Student: "Socrates! I have seen the truth! The world we live in is but a shadow—an illusion! My friends refuse to believe me. How can I make them see the light?"

+ ["Tell them there's free wine outside the cave. Works every time."] -> PathC
+ ["Truth must be discovered, not forced."] -> PathA
+ ["Drag them out! They’ll thank you later."] -> PathB




=== PathA ===
Student: "But what if they refuse to listen? What if they mock me, dismiss me?"

+ ["If they refuse, let them stay in darkness."] -> Outcome2
+ ["Tell them the Oracle of Delphi said the real world has better wine and sun"] -> Outcome3
+ ["True wisdom is patience; they must come to it themselves."] -> Outcome1


=== PathB ===
Student: "Yes! But… what if they resist? Must I fight to free them from ignorance?"

+ ["Maybe I was wrong. They must take the first step willingly."] -> Outcome1
+ ["Just tell them you saw a philosopher giving away free wine outside."] -> Outcome3
+ ["Ignorance is comfortable; they will fight to stay."] -> Outcome2


=== PathC ===
Student: "Huh… that… might actually work?"

+ ["If they don’t believe you, you can just go home and drink some wine."] -> Outcome3
+ ["No, it was a joke. Let them come to knowledge, not be tricked into it."] -> Outcome1
+ ["Hey, if it works, it works."] -> Outcome2




=== Outcome1 ===
Student nods, accepting the wisdom. Socrates gains reputation
~ raise (correct)
-> END


=== Outcome2 ===
Student frowns, disappointed in Socrates’ pragmatism. Citizens will be less likely to aid him in future.
~ raise (incorrect)
-> END


=== Outcome3 ===
Student laughs and decides absurdity might open minds. Students talk about Socrates humor. He is now deemed to be a funniest guy in the city.
~ raise (humorous)
-> END
