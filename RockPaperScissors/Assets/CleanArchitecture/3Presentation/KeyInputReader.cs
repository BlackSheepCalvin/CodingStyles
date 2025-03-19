using UnityEngine;

// Hint: Single responsibility principle (input reading is not part of Initializer)
// Hint: possible to do observer pattern so multiple users could subscribe
// Hint: making things as generic as possible: no mention of Variations here, just inputUsers that can be anything that confirms to KeyInputUser
public class KeyInputReader : MonoBehaviour
{
    private KeyInputUser inputUser;
    private bool isPaused; // Hint: isPaused is a better name than isActive, because isActive would make you use a bunch of negations in the code
    // like: isActive = inputUser != null , if (!isActive) { return; }

    public KeyInputUser InputUser {
        get => inputUser;
        set {
            inputUser = value;
            isPaused = inputUser == null; // Hint: side effect... I dont like them normally, but if they simplify things, why not?
        }
    }

    void Update()
    {
        if (isPaused) { return; }

        if (Input.anyKeyDown)
        {
            inputUser.DidPressKey(Input.inputString);
        }
    }
}