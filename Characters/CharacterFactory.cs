using System;

namespace Sprint0.Characters;

public class CharacterFactory
{
    private static CharacterFactory instance = new CharacterFactory();


    public static CharacterFactory Instance
    {
        get
        {
            return instance;
        }
    }
    private CharacterFactory()
    {

    }

    void CreateKrisCharacter()
    {
        
    }
}
