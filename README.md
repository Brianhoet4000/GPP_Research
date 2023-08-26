# Realistic and Engaging Enemy AI Shooting Accuracy in Games

## Intro
When it comes to making enemy AI for first-person shooter games, it's easy to turn them into unstoppable killing machines. But that wouldn't be fair to players. To offer players a balanced challenge, enemies need to miss some shots intentionally. This project aimed to figure out how to design shooting AI that gives players a fair experience while also keeping the fact that missed shots are intentional less obvious. As someone who loves playing single-player games, I set out on this journey of exploration.

## Theory

This method offers a wonderful opportunity for personalized adjustments, allowing each person to create a unique AI experience even when pursuing the same goal. Although the core implementation remains simple, there's room for a lot of fine-tuning. As enemies shoot at the player, a hidden timer comes into play, determining the perfect moment for an enemy to take an accurate shot that hits the mark flawlessly. This timer, called the "hit delay," controls the timing of these precise shots. The formula for calculating this delay is as follows:

![Functionrule](https://github.com/Brianhoet4000/ggp_Research/assets/113976082/e8922f69-fcd1-4835-b503-c6cf945fac6c)

Where the delay base is a chosen value and the rules are functions where a calculated weight is created. Which also can be different for implementation to implmentation. Some rule examples:
- Distance between the player and enemy
- Velocity change of the player
- Player's state
- The amount of enemies around player (Token system)

### Token System
To address the issue of instant defeats, a strategy involves having AIs alternate their attacks on the player. This is achieved by passing around a logical token among the different agents engaged in combat. These tokens regulate various actions, but their purpose remains consistent: Only AIs holding a token can perform the specified action. For instance, we might decide that only token-holding AIs can shoot their weapons.

Imagine a single token for the entire game. This means only one AI can shoot at any given moment. Once shooting is done, the token holder releases it, allowing another agent to take a turn. However, this approach can lead to situations where AIs seem unrealistically positioned for shooting yet don't attempt it because they're evidently waiting for their turn.

In essence, while this method avoids one-hit kills, it could result in odd behavior where AIs seem to be in perfect positions for shots but don't take them due to the turn-based system.

  
