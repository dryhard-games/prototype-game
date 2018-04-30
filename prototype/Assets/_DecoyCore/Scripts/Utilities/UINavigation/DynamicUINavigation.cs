namespace Decoy.Core.Utilities {
    using UnityEngine.UI;
    /// <summary>
    /// DynamicUINavigation.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public static class DynamicUINavigation {
        /// <summary>
        /// Set the navigation of a dynamic button group, based on sibling index.
        /// </summary>
        /// <param name="buttons">Array with dynamic Buttons</param>
        /// <param name="staticSelectableTop">Static button for the up navigation of the first dynamic button</param>
        /// <param name="staticSelectableBottom">Static button for the bottom navigation of the last dynamic button</param>
        public static void SetNavigationDynamicButtonGroup(Button[] buttons, Button staticSelectableTop, Button staticSelectableBottom) {
            for (int i = 0; i < buttons.Length; i++) {
                int index = buttons[i].transform.GetSiblingIndex();

                Navigation navigation = new Navigation {
                    mode = Navigation.Mode.Explicit,
                    selectOnUp = staticSelectableTop,
                    selectOnDown = staticSelectableBottom
                };

                foreach (Button button in buttons) {
                    if (index != 0) {
                        if ((index - 1) == button.transform.GetSiblingIndex())
                            navigation.selectOnUp = button;
                    }

                    if (index != buttons.Length) {
                        if ((index + 1) == button.transform.GetSiblingIndex())
                            navigation.selectOnDown = button;
                    }
                }

                if (index == 0) {
                    Navigation nav = staticSelectableTop.navigation;
                    nav.selectOnDown = buttons[i];
                    staticSelectableTop.navigation = nav;
                }

                if (index == buttons.Length - 1) {
                    Navigation nav = staticSelectableBottom.navigation;
                    nav.selectOnUp = buttons[i];
                    staticSelectableBottom.navigation = nav;
                }

                buttons[i].navigation = navigation;
            }
        }
    }
}
