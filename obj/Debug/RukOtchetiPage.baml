   M S B A M L   `   `   `  ����C  ?MedLabUP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MedLabUPMedLabUP  5          MedLabUP.RukOtchetiPage   5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:mt1http://materialdesigninxaml.net/winfx/xaml/themes  5      FmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:MedLabUP     5        N� Title$  RukOtchetiPage��5       N� 
Background �#B1DFFF � 	Resources 5         RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius��%�   &	        �� 5   
   " ��6+   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $ 30��6!   #��z��63   $��0q�6]   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      5      5      .��5      �  � RowDefinitions 5   
   �� 5       �� 5!      $��4* �6   �� 5"      $��2* �6   �� 5#      .+�5&   
   �� $ 15��6   
�������$��0q�66   $��	50 0 50 5q�6J   $��1��6]   
�������$	��Top=�6~   ._�5'      �� -   5        	 ��Name$	 choose_otchet��5'      $��70��6.   )  #MaterialDesignThemes.Wpf.HintAssist
   Hint$
     Отчёт��6:   $��25(�6Y   $��Inter)�6g      HelperTextFontSize$ 70Q�6}   $��Center=�6�   
�������$��0q�6�   6�   5(      � 5+   
   $��2��6    � ColumnDefinitions 5,      �� 5-      $
��Auto �6#   �� 5.      $��* �6#   �� 5/      $
��Auto �6#   �� 50      $��* �6#   .+�53      �� -   5         ��Name$ period_s��53      $��Период с��6*   $��50(�6:   $��Inter)�6H   
���   �$��50,0,5,0q�6o   $��Center=�6�   $��0��6�   �� 55      -   5         ��Name$ border_s��55   '   $ 20��6   
�������
�������$��1q�6_   $��50��6s   $��
10,0,15,10q�6   $��1��6�   ._�56      (  "System.Windows.Controls.DatePicker  -   5          Name$ s_data��56      $��20(�6.   $��Inter)�6<   
���   �
�������$��Center=�6v   $��0q�6�   $��10,0,0,0q�6�   57      �� 59      -   5        $ 	period_po��59      $
��по��6+   $��50(�65   $��Inter)�6C   
���   �$��5,0,5,0q�6j   $��Center=�6{   $��2��6�   �� 5;      -   5        $ 	border_po��5;      $ 20��6(   
�������
�������$��1q�6`   $��50��6t   $��
10,0,50,10q�6�   $��3��6�   ._�5<        -   5        $ po_data��5<      $��20(�6/   $��Inter)�6=   
���   �
�������$��Center=�6w   $��0q�6�   $��10,0,0,0q�6�   5=      5>      � 5A   
   $��3��6    5C      �� 5D      $��3* �6#   �� 5E      $��5* �6#   .+�5H      �� -   5         ��Name$ download_pdf��5H      $��30 10 10 10q�6+   $��Скачать PDF��6@   $��20(�6V   ���#3A79A6
�������$��Inter)�6�   $��3��6�   #��[  6�   $��55��6�   $��0q�6  
�������6  5I      5J      5K      