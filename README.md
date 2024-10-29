To maintain a good clean structure of my project I chose to set it up with the dependancy injection framework VContainer. I aimed to create components with logic as separated and modular as possible which definately cost a lot of time while also being a great learning experience.

Not being really used to VContainer and with just a small amount of previous experience with other dependancy injection frameworks it was hard to map out a flow chart before starting with the project, which led me to experiment a bit of how to best set it up.

My aim was to set up a game architecture with a game manager managing the application state and sub managers handling separate systems, such as a LevelManager, AudioManager, AssetLoadingManager, MenuManager, UIManager and so on.

I spent quite a lot of time on the first few modules and spiraled away into ideas on how I could extend upon for instance the weapon system which led me astray from the main focus of the project and resulted in me having less time left further on in the project.

Another big time waster was that I spent a lot of time overthinking about how I could improve very minor things and make things more modular and a lot of it just ended up with me scrapping what had become overcomplicated and not particularly useful.

I didnt really end up utilizing the GameManager but I set up a quite simple LevelManager which manages the LevelGameState. I set up a notifier class that would let any other clases that needed to know about the current level game state to register to the notifier so it could update them. It was sort of like using an eventsystem but with interfaces.

I used this approach with notifiers and registering/unregistering for quite a few systems such as:
- Wrapping transforms to opposite sides when moving of the screen.
- Recycling transforms to pools when moving of the screen.
- Listening to when asteroids was destroyed.

The Level game states sort of bind the game logic into a game loop and controls what can be done in what states but it definately needs some love and attention as it is currently very unfinished.

There are currently sort of "rounds" implemented in the game, basically you must reach the score * (your current level) to come to the next round.
I have a simple save system implemented which i planned to build out on and create a more advanced version of where we could save/retrieve data simply and also make sure stuff is saved to avoid losing any data but I didn't really finnish it and as it is currently I could have just used playerPrefs and basically got the same results.

I did really like the challenge of the code challenge but I think i got a bit stuck in my own head with doubts and ended up not coming as far as i wanted.

It was fun to play around with Unit testing a little bit, even though i pretty mush just used it in the beginning for this project and then ended up being a little bit too stressed to utilize it for the rest of the project. This is definately something I want to become more proficient and experienced in writing.

A weird issue I got stuck on was that one component "AsteroidSplitter" wasn't recognized by VContainer or something as it wasn't registered even when it should have been, I had to add an "IStartable" interface to it and put some barebones logic in there for the Component to actually be registered (This was even tho this class listened to an interface from another class and performed an action based on it).



