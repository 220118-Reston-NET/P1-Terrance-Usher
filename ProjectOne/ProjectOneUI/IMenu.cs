namespace ProjectOneUI
{
    public interface IMenu
    {
        
        /// <summary>
        /// This will display the menus for ProjectOne.
        /// </summary>
        void Display();

        /// <summary>
        /// This will take in what choice the user picks in the menu
        /// </summary>
        /// <returns> Menu that will change the screen </returns>
        string UserChoice();

    }
}