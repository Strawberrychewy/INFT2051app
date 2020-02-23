using System;
using System.Collections.Generic;
using System.Text;

namespace INFT2051app {
    public class FamiliarsList {
        /*
         * This class holds all base pet information upon initialization
         * 
         *
         *
         */

        readonly List<BasePet> Familiars = new List<BasePet>();
        public FamiliarsList() {
            LoadJSON();
        }

        public void LoadJSON() {
            /*
             * This function takes the JSON file and turns each row into objects. This objects are added to the list.
             * Familiars.add(e)
             */
            Familiars.Add(new BasePet("Robbit", "Land", null, null, "It is said from the mountains and the trees that Robbit The Rabbit came out of hiding, watching the very world that would be his very own. His small stature is a sign that even the smallest creatures can become anything they want to be."));

            Familiars.Add(new BasePet("Rockworm", "Land", null, "Roccoon", "This evasive creature digs below the earth, protecting itself using rocks as plate armor"));
            Familiars.Add(new BasePet("Roccoon", "Land", "Rockworm", "Shellwyrm", "Preparing itself for further evolution, it patiently waits inside its spiked armor shell"));
            Familiars.Add(new BasePet("Shellwyrm", "Land", "Roccoon", null, "Unusually large for a small evolution chain, this large dragon uses its armor for offense and defense"));

            Familiars.Add(new BasePet("Sealpronger", "Sea", null, "Sealancer", "It uses its fork-like prong on its head to fish underwater for its next meal"));
            Familiars.Add(new BasePet("Sealancer", "Sea", "Sealpronger", "Sealskeer", "Sealancers charge at each other with the lances attached to their head as a sign of respect and nobility"));
            Familiars.Add(new BasePet("Sealskeer", "Sea", "Sealancer", null, "Its lance-like horns grow naturally and are continuously replaced.  When they fall off, Sealskeer uses them as leverage to propel itself across ice floes"));

            Familiars.Add(new BasePet("Elechick", "Air", null, "Wingspark", "This bird narrows down its enemies from above, stunning them with lightning fast talons"));
            Familiars.Add(new BasePet("Wingspark", "Air", "Elechick", "Thundersparse", "It generates electricity by flapping its wings, reminiscent of fireworks when seen in the night sky"));
            Familiars.Add(new BasePet("Thundersparse", "Air", "Wingspark", null, "Its incredible speed scatters electric sparks during flight that clap towards the earth like thunderstorms"));

        }

        public BasePet FindPetByName(string name) {
            /*
             * This function finds the pet with the exact name input. Used for Evolution
             */
            return Familiars.Find(x => x.Name == name);
        }

        public BasePet FindRandomBasicPet() {

            //Generates pet based on current list
            List<BasePet> basicpets = new List<BasePet>();
            foreach (BasePet pet in Familiars) {
                if (pet.EvolvesFrom == null) {
                    basicpets.Add(pet);
                }
            }

            //Finds a random pet in list
            Random random = new Random();
            int i = random.Next(0, basicpets.Count);
            return basicpets[i];
        }
    }
}
