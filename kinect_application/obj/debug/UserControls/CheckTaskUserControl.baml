   M S B A M L   `   `   `  ����M  IKINECT_APPLICATION, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:KINECT_APPLICATION.UserControlsKINECT_APPLICATION.UserControls  5      :    4KINECT_APPLICATION.UserControls.CheckTaskUserControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?local-clr-namespace:KINECT_APPLICATION.UserControls     5      .��5
      �� $��50q�6     �� Orientation$  Vertical=�6   .+�5   
   �� $��0,0,0,10q�6   $  
Horizontal=�6'   .+�5      �� $	��100��6   .��6    Name6&   �� 5      -   5        $
��Name��5   8   $��Center=�6   $	��882��6D   5      �� 5   
   $  
Horizontal=�6   .+�5      �� -   5        $��taskContent��5   S   $	��250��6:   $	��405��6F   $��0,0,10,0q�6f   �� 5      -   5        $��oCanvas��5   ,   
����ح�$	��720��6;   $	��405��6G   .+�5      �� -   5        $��oCamera��5   0   
����ح�$	��720��6?   $	��405��6K   5      5      �� 5   
   $��	0,10,0,10q�6   $  
Horizontal=�6(   .+�5      �� -   5        $
��Play��5   '   {�.  
�������$��25��6Y   $	��250��6e   .��6q   Play6w   �� 5      $��10,0,0,0q�6   $  
Horizontal=�6+   .+�5      �� $	��100��6   .��6$   Pain6*   � 5      -   5        $��ProgressPain��5      $��10q�62    � Minimum$ 0Q�6>    � Maximum$ 10Q�6J    � Value$	 0.0Q�6W   $	��125��6c   �� 5      $��Center=�6i   $��Center=�6�   ��6   ��  �� ElementName$ ProgressPain�� �� Path$ Value� �� StringFormat$ {0:N2}��6�   �� 5      -   5        $��Clear��5   +   $��10,0,0,0q�6   
�������$��25��6_   $	��250��6k   .��6w   Clear Trajectory6�   5      5       �� 5"   
   $��0,0,0,10q�6   $  
Horizontal=�6'   .+�5$      �� $  
Horizontal=�6   .+�5%      �� -   5        $��FrameSliderNumber��5%      $��50��61   $
��Left=�6<   .��6^   16a   �� 5&      -	   5        $��FrameSlider��5&   b   {�.  $��Center=�6G   $ 1Q�6u   $	��200��6�   5'      �� 5)      $��10,0,0,0q�6   $  
Horizontal=�6+   .+�5*      �� $	��100��6   .��6$   	Fatigue6-   � 5+      -
   5        $��ProgressFatigue��5+      $��10q�65   $ 0Q�6A   $ 10Q�6M   $	 0.0Q�6Z   $	��125��6f   �� 5,      $��Center=�6l   $��Center=�6�   ��6   �� $ ProgressFatigue��$ Value�$ {0:N2}��6�   5-      5.      �� 50   
   $��0,0,0,10q�6   $  
Horizontal=�6'   .+�51      �� -   5        
�������$��25��51   =   $	��250��6I   .��6U   Cancel6]   �� 52      $��10,0,0,0q�6   $  
Horizontal=�6+   .+�53      �� $	��100��6   .��6$   Mood6*   � 54      -   5        $��ProgressMood��54      $��10q�62   $ 0Q�6>   $ 10Q�6J   $	 0.0Q�6W   $	��125��6c   �� 55      $��Center=�6i   $��Center=�6�   ��6   �� $ ProgressMood��$ Value�$ {0:N2}��6�   56      57      59      5;      