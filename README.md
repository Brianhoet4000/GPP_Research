# Realistic and Engaging Enemy AI Shooting Accuracy in Games

## Intro
When it comes to making enemy AI for first-person shooter games, it's easy to turn them into unstoppable killing machines. But that wouldn't be fair to players. To offer players a balanced challenge, enemies need to miss some shots intentionally. This project aims to figure out how to design shooting AI that gives players a fair experience while also keeping the fact that missed shots are intentional less obvious. As someone who loves playing single-player games, I set out on this journey of exploration.

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

## Implementation

I've successfully translated the theory into a functional Unity project, complete with a visual representation of distance through a dynamic spotlight. As the player approaches the 20-meter mark, the spotlight transitions from green to yellow, and then to red at the 10-meter mark. Additionally, players can observe their health decreasing through a health bar when they are hit.

My implementation features two distinct rules. The first rule is based on the distance between the player and the enemy. This rule employs the changing spotlight colors as a multiplier. As the player gets closer, the multiplier escalates, effectively impacting AI behavior.

The second rule accounts for the player's velocity and movement direction relative to the enemy. If the player is moving toward the enemy, the AI behavior is influenced one way. Conversely, if the player is moving away, the behavior shifts accordingly.

![HitDelay](https://github.com/Brianhoet4000/ggp_Research/assets/113976082/964c4011-8b15-417b-bf73-f1c9484a9a37)

To provide you with a visual understanding, I've included graphs below that depict the multiplier variations in accordance with the aforementioned rules. This visual aid serves to elucidate how the AI behavior adapts based on distance and player movement, enriching the overall gaming experience.

![DistanceRule](https://github.com/Brianhoet4000/ggp_Research/assets/113976082/987e7f99-b66f-457b-aff3-afe6256bbab1)
![VelocityRule](https://github.com/Brianhoet4000/ggp_Research/assets/113976082/ad55ab33-c577-4af2-919f-0bcb4f91477a)

## Conclusion

The implementation has worked well. It's clear both when you play the build and from the graph below. The turret fires 40 rounds in a magazine and shoots at a rate of 6 rounds per second. At long range, it hits 5 rounds, 7 rounds at medium range, and an impressive 9 rounds up close.

This setup has great potential, but getting the right feel that's fair to the player requires careful tuning. You can also add more rules to adjust the hit delay, making the system even more unique and adaptable.

![Result](https://github.com/Brianhoet4000/ggp_Research/assets/113976082/4860573b-408f-4cfc-9391-e28c50b96ca8)

Source:

https://core.ac.uk/download/pdf/33504361.pdf
https://www.gameaipro.com/GameAIPro3/GameAIPro3_Chapter33_Using_Your_Combat_AI_Accuracy_to_Balance_Difficulty.pdf
