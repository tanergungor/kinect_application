   M S B A M L   `   `   `  ����M  IKINECT_APPLICATION, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:KINECT_APPLICATIONKINECT_APPLICATION  5      1    +KINECT_APPLICATION.InsertPatientUserControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:KINECT_APPLICATION     5      .��5
      �� $��50q�6     �� Orientation$  
Horizontal=�6   .+�5   
   �� $��0,0,25,0q�6   $  Vertical=�6'   .+�5      �� -   5        $��Photo��5      $��0,0,0,0q�6!   $9��3pack://application:,,,/Resources/PHOTOS/PROFILE.png��62   $��96��6o   $��64��6{   $	��Top=�6�   $��Center=�6�   �� 5      -   5        
�������$��25��5   @   .��6L   Load6R   5      �� 5   
   $��0q�6   $  Vertical=�6    .+�5      �� $��0,0,0,10q�6   $  
Horizontal=�6+   .+�5      �� $	��100��6   .��6$   Name6*   �� 5      -   5        $
��Name��5   <   $��Center=�6   $	��150��6H   5      �� 5      $��0,0,0,10q�6   $  
Horizontal=�6+   .+�5      �� $	��100��6   .��6$   	Surname6-   �� 5      -   5        $��Surname��5   <   $��Center=�6   $	��150��6K   5      �� 5      $��0,0,0,10q�6   $  
Horizontal=�6+   .+�5      �� $	��100��6   .��6$   Phone6+   �� 5      -   5        $��Phone��5   <   $��Center=�6   $	��150��6I   5       �� 5"      $��0,0,0,10q�6   $  
Horizontal=�6+   .+�5#      �� $	��100��6   .��6$   E-mail6,   �� 5$      -   5        $��Email��5$   <   $��Center=�6   $	��150��6I   5%      �� 5'      $��0,0,0,10q�6   $  
Horizontal=�6+   .+�5(      �� $	��100��6   .��6$   	Birthdate6/   (  "System.Windows.Controls.DatePicker  5)      -   5        $��	Birthdate��5)      $	��150��6.   6<   5*      �� 5,      $��0,0,0,10q�6   $  
Horizontal=�6+   .+�5-      �� $	��100��6   .��6$   Gender6,   �� 5.      -   5        $��Gender��5.      $	��150��6)   .@�5/      �� .��6#   Female6+   �� 50      .��6#   Male6)   51      52      �� 54      $��0,0,0,10q�6   $  
Horizontal=�6+   .+�55      �� $	��100��6   .��6$   Height (cm)61   �� 56      -	   5        $��Height��56   <   $��Center=�6   $	��150��6J   6V   57      �� 59      $��0,0,0,10q�6   $  
Horizontal=�6+   .+�5:      �� $	��100��6   .��6$   Weight6,   �� 5;      -
   5        $��Weight��5;   <   $��Center=�6   $	��150��6J   6V   5<      �� 5>      $��0,0,0,10q�6   $  
Horizontal=�6+   .+�5?      �� -   5        
�������$��25��5?   H   $	��250��6T   .��6`   Insert6h   5@      �� 5B      $��0,0,0,10q�6   $  
Horizontal=�6+   .+�5C      �� -   5        
�������$��25��5C   A   $	��250��6M   .��6Y   Cancel6a   5D      5F      5H      5J      