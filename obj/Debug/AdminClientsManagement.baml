   M S B A M L   `   `   `  ����C  ?MedLabUP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MedLabUPMedLabUP  5      %    MedLabUP.AdminClientsManagement   5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:mt1http://materialdesigninxaml.net/winfx/xaml/themes  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:MedLabUP     5        N� Title$  AdminClientsManagement��5
       N� 
Background �#B1DFFF � 	Resources 5         RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius  RoundedComboBoxStyle d�Name	 �� 	ClickMode
 d� 	IsChecked �� Path �� Mode �� RelativeSource � ColumnDefinitions ��Name J� IsHitTestVisible F�Name �Name � AllowsTransparency ��Name-  'System.Windows.Input.KeyboardNavigation   TabNavigation �� Triggers P� Property P� Value �� 
TargetName��%�  &	        &	 �     �� 5   
   " ��6+   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $ 35��6!   #��z��63   $��0q�6]   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      5      �� 5   
   " ��6-   .�5      ��  � ��
 �����6+   �� 5       � ��$ 10q�6(   �� 5       � ��$ 15q�6'   �� 5       � �� 5      �� 5       " ��6&   .R�5!      � .+�5"      �� $ 10��6%   #��z��67   $��1q�6a   ���#CCCCCC._�5#   "   d� $ ToggleButton��6/   $��0q�5$   +   
������ |�.  $	 Press=�5(   +   
 5&   +   �� $ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+.��5)   &   �  5*   *   �� 5+   .   �� 5,   .   $
��Auto �6?   .+�5.   *   �� $ PART_ContentPresenter��6K   $��0��6;    .  $��3q�6�   $
��Left=�6�   $��Center=�6�   F� 5/   *   $ arrow��6?   $��1��6/   $��0,0,10,0q�6N   ���#888"����        � 	=  	= � z     	$��Center=�6�   $��Center=�6�   50   '   52   #   54      � 55      $ 
PART_Popup��6%   $��Bottom=�69    . |�.  $
��Fade=�6�   ��6M   �� $ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+.��56   "   �� 
�������$��1q�6<   ���#CCCCCC._�57   &   �� $ DropDownScrollViewer��63   .��58   *   �� $ Cycle=�69   59   '   5:   #   5;      5<       5=      P� 5>       � {� .  . �5?   "   ��  � �� �#8886?   5@      P� 5A       �@J�IsMouseOver . . �5B   "   ��  � �� �#9996@   �� 5C   "   $ arrow��6)    � �� �#3336L   5D      5H      5J      5K      5M      .��5O      �  � RowDefinitions 5P   
   �� 5Q      $
��0.3* �6   �� 5R      $��5* �6    5U   
   �� 5V      $��4* �6   �� 5W      $��2* �6   .+�5Z   
   � $��1��6   $��1��6   $��15q�6,    5[      �� 5\      $��3* �6    �� 5]      $
��Auto �6    .+�5`      �� $��1q�6   .+�5b      �� -   5         ��Name$ create_ur_btn��5b      $$��Добавить юр лицо��60   ���#3A79A6
�������$��Inter)�5c   0   $��1��6C   #��[  5d      $��60��65   $��30(�6A   $��
20,0,15,10q�6P   �� 5g      -   5        $ edit_ur_btn��5g      $$��Изменить юр лицо��6.   ���#3A79A6
�������$��Inter)�5h   4   $��1��6G   #��[  5i      $��60��69   $��30(�6E   $��
20,0,15,90q�6T   �� 5k      -   5        $ create_fiz_btn��5k      $&�� Добавить физ лицо��61   ���#3A79A6
�������$��Inter)�5l   5   $��1��6H   #��[  5m      $��60��6:   $��30(�6F   $��
20,0,15,10q�6U   �� 5o      -   5        $ edit_fiz_btn��5o      $&�� Изменить физ лицо��6/   ���#3A79A6
�������$��Inter)�5p   5   $��1��6H   #��[  5q      $��60��6:   $��30(�6F   $��
20,0,15,10q�6U   5t      �� 5x      -   5        $ 
delete_btn��5x      $��Удалить��6)   ���#3A79A6
�������$��Inter)�5y   9   $��2��6L   #��[  5z      $��60��6>   $��30(�6J   $��
20,0,15,10q�6Y   5|      �� 5~   
   ���#0077DE$��2q�6'   $ 5��6;   $��1��6L   $��15q�6Y   ._�5      &   System.Windows.Controls.DataGrid  -   5          Name$ 	staffinfo��5         AutoGenerateColumns .  $��1��6F      
IsReadOnly .    Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn    Header$  ФИО��6)   !   Width5  /System.Windows.Controls.DataGridLengthConverter$! 4* 6S   1  +System.Windows.Controls.DataGridBoundColumn"   Binding" 66   �� *
FullName+6S     5�      $  
Почта��6)   $! 6* 6R   " 68   �� *Email+6R   5�      5�      5�      5�      