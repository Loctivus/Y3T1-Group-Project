

LIST questAreas = (Graveyard) , (Academy) , (Swamp), (DragonDen) , (EnchantedForest)
VAR qAreas = ()

VAR currentArea = ""

===QuestRumourHub===
~qAreas = questAreas
I heard a rumour...

{LIST_RANDOM(qAreas):
    - Graveyard:
        -> TheGraveyard
        
    - Academy:
        -> TheAcademy
        
    - Swamp:
        -> TheSwamp
        
    - DragonDen:
        -> TheDragonDen
        
    - EnchantedForest:
        -> TheEnchantedForest
}



=TheGraveyard
    Some spirits started risin from their graves, praising some weird mystical cup.
    It's gotta be worth somethin if it's raising the dead over it.
    ~questAreas -= Graveyard
    -> DONE
    
=TheAcademy
    Those wizards over at the academy were doin some weird ritual up in their tower.
    Somethin to do with all those silk tapestries they got inside that place.
    You'd think wizards would have a decent idea of interior decoration, but I guess not.
    ~questAreas -= Academy
    -> DONE
    
=TheSwamp
    A shipment of some grog got stranded in the swamp and they can't get it back.
    Bit of a shame, I heard that was some top-tier booze.
    ~questAreas -= Swamp
    -> DONE
    
    
=TheDragonDen
    ayo thats a dragon
    nah wait maybe a wyvern
    nope thats a dragon
    ~questAreas -= DragonDen
    -> DONE


=TheEnchantedForest
    that do be a spooky forest
    keeps glowing at night
    weird spots of light floating around
    ~questAreas -= EnchantedForest
    -> DONE













