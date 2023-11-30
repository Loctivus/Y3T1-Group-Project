INCLUDE Quest Rumours.ink


LIST gavinTexts = (GavinText1) , (GavinText2), (GavinText3)
VAR gavTexts = ()
VAR metGavin = false

LIST tamsinTexts = (TamsinText1) , (TamsinText2) , (TamsinText3)
VAR tamTexts = ()
VAR metTamsin = false

LIST varadonTexts = (VaradonText1) , (VaradonText2) , (VaradonText3)
VAR varaTexts = ()
VAR metVaradon = ()

===Gavin===
~gavTexts = gavinTexts

{metGavin == false:
    {LIST_RANDOM(gavTexts):
    - GavinText1:
        ->GavText1
    
    - GavinText2:
        ->GavText2
        
    - GavinText3:
        ->GavText3
    }
    
 - else:
    Hey! It's me again! been a little while.
    -> QuestRumourHub
}

//-> DONE

=GavText1
    Hey mate! I'm Gavin, pleasure to meet ya!
    ~metGavin = true
    
    -> QuestRumourHub    

=GavText2
    Hey there bud! Name's Gavin!
    ~metGavin = true
    -> QuestRumourHub 

=GavText3
    'Ello mate! You've got a nice tavern here! Name's Gavin!
    ~metGavin = true
    -> QuestRumourHub 
    

===Tamsin===
~tamTexts = tamsinTexts

{metTamsin == false:
    {LIST_RANDOM(tamTexts):
        - TamsinText1:
            ->TamText1
    
        - TamsinText2:
            ->TamText2
            
        - TamsinText3:
            ->TamText3
    }
    
 - else:
    Ah, a pleasure to meet you again, barkeep.
    -> QuestRumourHub
}
    
    
=TamText1
    Hello, my name is Tamsin, pleasure to make your acquaintance.
    -> QuestRumourHub

=TamText2
    Greetings, I am Tamsin, pleasure to meet you.
    -> QuestRumourHub
    
=TamText3
    Good day, barkeep, my name is Tamsin.
    -> QuestRumourHub
    

===Varadon===
~varaTexts = varadonTexts

{metVaradon == false:
    {LIST_RANDOM(varaTexts):
        - VaradonText1:
            ->VaraText1
            
        - VaradonText2:
            ->VaraText2
        
        - VaradonText3:
            ->VaraText3
    }
    
 - else:
    Hmph, looks like you are still here.
        -> QuestRumourHub
}

=VaraText1
    Greetings, barkeep. I expect a good service.
    -> QuestRumourHub
    
=VaraText2
    Hmph, hello barkeep. I am Varadon.
    -> QuestRumourHub
    
=VaraText3
    Hello, I am Varadon. Now hurry and get us a drink.
    -> QuestRumourHub
    
    
    
    
    
    
    
    
    

