   M S B A M L   `   `   `  ����M  IKINECT_APPLICATION, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:KINECT_APPLICATIONKINECT_APPLICATION  5      0    *KINECT_APPLICATION.SelectDoctorUserControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:KINECT_APPLICATION     5      .��5
      �� $��50q�6     �� Orientation$  
Horizontal=�6   .+�5   
   �� $  Vertical=�6   .+�5      �� $��0,0,0,25q�6   $  
Horizontal=�6+   .+�5      �� -   5        $��Welcome��5      $��24(�6+   ���#33CCFF	WELCOME6W   5      �� 5      $  
Horizontal=�6   .+�5      �� $��0,0,25,0q�6   $  Vertical=�6/   .+�5      �� -   5        $��Photo��5      $��0,0,0,0q�6)   $9��3pack://application:,,,/Resources/PHOTOS/PROFILE.png��6:   $��96��6w   $��64��6�   $	��Top=�6�   $��Center=�6�   �� 5      -   5        
�������$��25��5   H   .��6T   Load6Z   5      �� 5      $��0,0,25,0q�6   $  Vertical=�6/   .+�5      �� $��0,0,0,10q�6!   $  
Horizontal=�63   .+�5      �� $	��100��6    .��6,   Name62   �� 5      -   5        $
��Name��5   D   $��Center=�6"   $	��150��6P   5      �� 5!      $��0,0,0,10q�6!   $  
Horizontal=�63   .+�5"      �� $	��100��6    .��6,   	Surname65   �� 5#      -   5        $��Surname��5#   D   $��Center=�6"   $	��150��6S   5$      �� 5&      $��0,0,0,10q�6!   $  
Horizontal=�63   .+�5'      �� $	��100��6    .��6,   Phone63   �� 5(      -   5        $��Phone��5(   D   $��Center=�6"   $	��150��6Q   5)      �� 5+      $��0,0,0,10q�6!   $  
Horizontal=�63   .+�5,      �� $	��100��6    .��6,   E-mail64   �� 5-      -   5        $��Email��5-   D   $��Center=�6"   $	��150��6Q   5.      �� 50      $��0,0,0,10q�6!   $  
Horizontal=�63   .+�51      �� $	��100��6    .��6,   	Birthdate67   (  "System.Windows.Controls.DatePicker  52      -   5        $��	Birthdate��52   %   $	��150��66   6D   53      �� 55      $��0,0,0,10q�6!   $  
Horizontal=�63   .+�56      �� $	��100��6    .��6,   Gender64   �� 57      -	   5        $��Gender��57   #   $	��150��61   .@�58      �� .��6+   Female63   �� 59      .��6+   Male61   5:      5;      �� 5=      -
   5        
�������$��0,0,0,10q�5=   0   $��25��6B   .��6i   Update6q   �� 5?      -   5        
�������$��0,0,0,10q�5?   0   $��25��6B   .��6a   Exit6g   5A      5C      5E      �� 5I      $  Vertical=�6   .+�5K      �� $��0,0,0,10q�6   $  
Horizontal=�6+   $	��275��6D   .+�5M      �� $��PATIENT LIST��6   $��Center=�60    � LayoutTransform 5N      �� 5O       �� Angle$ 90Q�6*   5Q      &   System.Windows.Controls.DataGrid  5S      -   5        $��patientList��5S         HorizontalGridLinesBrush �#33CCFF   VerticalGridLinesBrush �#33CCFF   AutoGenerateColumns .     CanUserAddRows .  $	��700��6�   ��6�   ��  �� Path$ LoadDataBinding�   Columns 5U      0  *System.Windows.Controls.DataGridTextColumn  5V      ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID��6-   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 69   �� $ 	person_id�  5W      $
	 Name��6-   
 6;   �� $ person_name�  5X      $	 Surname��6-   
 6>   �� $ person_surname�  5Y      $	 E-mail��6-   
 6=   �� $ person_email�  5Z      $	 Gender��6-   
 6=   �� $ person_gender�  5[      $	 Date of Birth��6-   
 6D   �� $ person_birthdate�  5\      $	 Registration Date��6-   
 6H   �� $ person_registered_date�4  .System.Windows.Controls.DataGridTemplateColumn  5^         CellTemplate 5_      �� 5`   "   .R�5a   &   �� $��Select��6@   -   5        
�������$��0q�5a   j   .��6   �� $1��+pack://application:,,,/Resources/Select.png��6�   6�   5b   #   5d        5e       5f      �� 5g   "   .R�5h   &   �� $��Delete��6@   -   5        
�������$��0q�5h   j   .��6   �� $1��+pack://application:,,,/Resources/Delete.png��6�   6�   5i   #   5k      5m      5n      �� 5p      -   5        
�������$��25��5p   (   .��6P   Add New Patient6a   �� 5r      $��	0,10,0,10q�6   $  
Horizontal=�6,   $	��275��6E   .+�5t      �� $��EXERCISE LIST��6   $��Center=�61    5u      �� 5v      $ 90Q�6*   5x        5z      -   5        $��exerciseList��5z       �#33CCFF �#33CCFF .   .  $	��700��6�   ��6�   �� $ LoadDataBinding� 5{        5|      $	 ID��6-   
 69   �� $ exercise_id�  5}      $
	 Name��6-   
 6;   �� $ exercise_name�  5~      $
	 Path��6-   
 6;   �� $ exercise_path�  5�       5�      �� 5�   "   .R�5�   &   �� $��Select��6@   -   5        
�������$��0q�5�   k   .��6�   �� $1��+pack://application:,,,/Resources/Select.png��6�   6�   5�   #   5�        5�       5�      �� 5�   "   .R�5�   &   �� $��Delete��6@   -   5        
�������$��0q�5�   k   .��6�   �� $1��+pack://application:,,,/Resources/Delete.png��6�   6�   5�   #   5�      5�      5�      �� 5�      -   5        {�.  $��25��5�   '   .��6P   Add New Exercise6b   5�      5�      5�      