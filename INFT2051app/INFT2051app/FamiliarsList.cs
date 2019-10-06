using System;
using System.Collections.Generic;
using System.Text;

namespace INFT2051app {
    class FamiliarsList {
        /*
         * This class holds all base pet information upon initialization
         * Additionally it may contain some information like obtained/etc
         * 
         * 
         *
         *
         */

        readonly List<BasePet> Familiars = new List<BasePet>();
        public FamiliarsList() {

        }

        public void LoadJSON() {
            /*
             * This function takes the JSON file and turns each row into objects. This objects are added to the list.
             * THIS FUNCTION IS FAKE ATM DON'T SUE ME
             * Familiars.add(e)
             */

        }

        public void FindPetByName() {
            /*
             * This function finds the pet with the exact name input. Used for Evolution
             */
        }
    }
}
