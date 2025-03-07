VAR correct = 0
VAR incorrect = 0
VAR humorous = 0

-> Start 

 === function lower(ref x)
 	~ x = x - 1

 === function raise(ref x)
 	~ x = x + 1


=== Start ===
Merchant: "Socrates! Behold the Ring of Gyges! With this, you can become invisible—free to act as you wish, unseen and unjudged. What would you do?"

+ ["Would a just man use such power at all?"] -> PathA
+ ["Why resist temptation when there are no consequences?"] -> PathB
+ ["Finally! Now I can sneak into Xenophon's kitchen without him yelling at me."] -> PathC

=== PathA ===
Merchant: "Hmm… A fair question. But let’s say he did take it—would he truly remain just? Or would he, like all men, give in?"

+ "Justice is who we are, not just fear of consequences." -> Outcome1
+ "In the end, justice is just a fancy word for survival." -> Outcome2
+ "Personally, if I wore the ring,  I would sneak into the Symposium and drink all the wine without anyone noticing. For... philosophical research, of course" -> Outcome3


=== PathB ===
Merchant: "Ha! You speak the truth—power untethered by consequence corrupts even the best of us. But tell me, do you think people are just because they want to be or because they fear punishment?"

+ "True justice is who we are when no one is watching." -> Outcome1
+ "Justice is a tool to keep the weak in line." -> Outcome2
+ "Does the ring also let me dodge my debts? Asking for a friend." -> Outcome3


=== PathC ===
Merchant: "...Are you telling me that with unlimited power, your first thought is stealing grapes?"

+ "Even with power, discipline defines a man." -> Outcome1
+ "Well, what’s the point of power if not personal gain?" -> Outcome2
+ "Maybe it will be the second thing to do. The first one will be to sneak into the Symposium and drink all the wine without anyone noticing. For... philosophical research, of course" -> Outcome3


=== Outcome1 ===
Merchant nods approvingly and gifts Socrates the Ring of Gyges. 
~ raise (correct)
-> END


=== Outcome2 ===
Merchant keeps the ring, skeptical of Socrates’ view. Socrates loses reputation.
~ raise (incorrect)
-> END

=== Outcome3 ===
Merchant sighs at Socrates’ humor. No item gained, but merchants in the city now share stories of his humor.
~ raise (humorous)
-> END