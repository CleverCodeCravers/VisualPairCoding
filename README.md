# VisualPairCoding

VPC is a small windows application that will track for you whos turn it is in a mob or pair coding session.

## Description

The official recommendation for pair programming is to have one computer and one keyboard only.

But what we do far more often in todays home office driven world is that we connect to a dev machine remotely and then the switching is not done by passing around a keyboard but instead we simply "switch jobs".

Unfortunately in such a scenario it is easy to loose track on whose round it is or effectivly "take the keyboard away" from a person, that just can't let go. Also it is very helpful to know how much time is left.

This is where VisualPairCoding (VPC) steps in: 

- It shows in a small always-on-top-window whos turn it is and how much time remains.
- When the turn changes the screen will be blocked with a full screen window and the next person is called out and has to click "OK" to reveal the screen again.

## Features

- Up to 10 participants
- The length of the session is customizable (usually we use 5-7 minute turns, but knock yourself off)
- Skip current driver if he/she is absent (went out of the room for a turn)
- Animation with the name of the next "Driver" when roles change, so a new turn cannot be easily overlooked. We also added a button that the person that takes over should hit to further ensure that the next person in the group really has a chance to take over.
- Explicitly visual, so that it does work with visual remote control software like "Team Viewer".

## See it in action!

### The Main Menu of the App

![VisualPairCoding Main Menu](./Documentation/VisualPairCoding_MainMenu.png)

- You can load and save sessions for reuse through the session menu. (You can also start the application from the command line passing in a config file that you saved here. The session will then start automatically.)
- "Randomize participants" in case you want a more random order
- Start starts the session

### The Coding Session

- The window is always on top. Just move it above a part of your ide that you do not need to see.
- Move the window by clicking somewhere onto it and just drag it to the right place.

![VisualPairCoding Session](./Documentation/VisualPairCoding_SessionMenu.gif)

- And of course you need a stunning interaction when the driver changes.

![VisualPairCoding Session](./Documentation/VisualPairCoding_Animation.gif)

## Why do pair programming?

If you want to know more about why pair/mob programming makes sence, have a look here:
- https://www.youtube.com/watch?v=t92iupKHo8M Dave Farley
- https://www.youtube.com/watch?v=5ySLQ5_cQ34 Pair Programming: 7 Habits of Highly Effective Coders