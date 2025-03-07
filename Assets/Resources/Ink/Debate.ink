VAR correct = 0
VAR incorrect = 0
VAR humorous = 0
VAR npc_portrait = "Socrates"

-> Start 

 === function lower(ref x)
 	~ x = x - 1

 === function raise(ref x)
 	~ x = x + 1


=== Start ===
Juror 1: "Socrates, you stand accused of corrupting the youth of Athens. How do you plead?"
 + [Socrates: "I plead not guilty, for I have done nothing but seek the truth and encourage others to do the same."] -> Round11


=== Round11 ===
Juror 2: "Your so-called ‘truth-seeking’ has led young minds astray. They question everything—our traditions, our laws, even our gods! Is this not corruption?" 
+ [Socrates: "Is it corruption to teach others to think for themselves? Or is it corruption to demand they accept ideas without question?"] -> Round12

=== Round12 ===
Juror 3: "Enough riddles, Socrates! Answer plainly: do you deny that your teachings have caused the youth to disrespect their elders and the laws of Athens?"


+ ["If the youth are corrupted, it is not by my teachings but by the failings of their elders. Blame yourselves, not me."] -> Round1Incorrect
+ ["I do not deny that I encourage questioning. But is it not better to have a youth who thinks critically than one who blindly follows? A city of thinkers is stronger than a city of followers."] -> Round1Correct
+ ["If questioning is corruption, then I suppose every child who asks ‘Why is the sky blue?’ is a criminal!"] -> Round1Humorous



=== Round1Correct ===

The jury murmurs thoughtfully. Some nod in agreement, while others remain skeptical. 
~ raise (correct)
+ [Next Round] -> Round21


=== Round1Incorrect ===

The jury reacts with anger. "You dare blame us for your crimes?"
~ raise (incorrect)
+ [Next Round] -> Round21

=== Round1Humorous ===

The jury chuckles nervously. A few jurors smile, but others frown at Socrates’ irreverence.
~ raise (humorous)
+ [Next Round] -> Round21









=== Round21 ===
Juror 1: "Your words sound clever, Socrates, but they do not absolve you of guilt. You have taught the youth to question the gods themselves. Is this not impiety?"
+ [Socrates: "Is it impiety to seek understanding? Or is it impiety to assume we already know the minds of the gods?"] -> Round22

=== Round22 ===
"Juror 2: "You twist words like a snake, Socrates! Answer plainly: do you believe in the gods of Athens?"


+ ["If the gods are so easily offended by questions, perhaps they are not as powerful as we think."] -> Round2Humorous
+ ["I do not claim to know the will of the gods, but I see no evidence of their influence in our daily lives."] -> Round2Incorrect
+ [I believe in seeking wisdom, wherever it may lead. If the gods are wise, they will not fear my questions. To question is not to deny, but to understand."] -> Round2Correct




=== Round2Correct ===

The jury nods thoughtfully. Socrates’ humility and reasoning resonate with some jurors.
~ raise (correct)
+ [Next Round] -> Round31

=== Round2Incorrect ===

The jury reacts with outrage. "You deny the gods? This is blasphemy!"
~ raise (incorrect)
+ [Next Round] -> Round31

=== Round2Humorous ===

The jury laughs nervously. Some jurors are amused, but others see it as disrespectful.
~ raise (humorous)
+ [Next Round] -> Round31










=== Round31 ===
Juror 3: "Your words are persuasive, Socrates, but actions speak louder. Have you not caused harm by leading the youth away from the traditions that hold our city together?"
+ [Socrates: "If traditions are so fragile that they cannot withstand questioning, are they truly worth preserving? Or is it better to build a society on reason and virtue?"] -> Round32

=== Round32 ===
Juror 1: "You speak of virtue, yet you stand accused of undermining it. How do you answer this charge?"


+ ["Virtue is not blind obedience but the pursuit of wisdom and justice. If I have inspired the youth to seek these, how can that be a crime? A city that fears questions fears the truth."] -> Round3Correct
+ ["If challenging traditions is dangerous, then Athens should outlaw thinking entirely. It would make your job much easier, wouldn’t it?"] -> Round3Humorous
+ ["If I am guilty of anything, it is believing in the potential of the youth to become better than we are. But perhaps Athens is not ready for such change."] -> Round3Incorrect





=== Round3Correct ===

The jury is moved by Socrates’ defense. Many jurors nod in agreement, seeing his point.
~ raise (correct)
-> END

=== Round3Incorrect ===

The jury sees Socrates’ idealism as a threat to stability of the city.
~ raise (incorrect)
-> END

=== Round3Humorous ===

The jury chuckles, but the humor does little to sway their opinion.
~ raise (humorous)
-> END