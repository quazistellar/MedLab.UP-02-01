   M S B A M L   `   `   `  ����C  ?MedLabUP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MedLabUPMedLabUP  5          MedLabUP.StatusAnalyzPage   5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:mt1http://materialdesigninxaml.net/winfx/xaml/themes  5      FmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:MedLabUP     5        N� Title$  StatusAnalyzPage��5       N� 
Background �#B1DFFF � 	Resources 5         RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius��%�   &	        �� 5   
   " ��6+   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $ 35��6!   #��z��63   $��0q�6]   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      �� 5       $��25 �6   	 � ColumnDefinitions	 5"   
   �� 5#      $��15 �6   �� 5$      $
��1080 �6   �� 5%      $��* �6   .+�5(   
   �� ���#0077DE$��2q�6'   $ 5��6;   $��1��6L   $��1��6Y   ._�5)      &   System.Windows.Controls.DataGrid  -   5        
  Name$
 analyzes��5)         AutoGenerateColumns .  $��1��6E   $��1q�6S   $��0��6g      
IsReadOnly .    Columns 5*      0  *System.Windows.Controls.DataGridTextColumn  5+      ,  &System.Windows.Controls.DataGridColumn   Header$- 'Наименование анализа��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6f   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6G   �� *
AnalyzName+6f     5,      $ Пациент (ФИО)��6)   $ * 6c    6@   �� *ClientFullName+6c     5-      $ Статус��6)   $ * 6X    69   �� *
StatusName+6X   5/      50      � 52   
   $��2��6   $��1��6    53      �� 54      $��* �6    �� 55      $
��Auto �6    .+�58      �� $ 30��6   
�������$��60��6:   $��	20,0,15,0q�6G    �� Padding$ 10q�6Z   $��0��6g   $	��Top=�6t   $��Stretch=�6�   ._�59      �� -   5         ��Name$ change_status_cbx��59      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Статус��66   $��20(�6R   $��Inter)�6`    �� DisplayMemberPath$ NameStat��6s   
���   �
������� �� SelectedValuePath$ ID_StatusOrder��6�   $��0,7,0,0q�6�   $	��Top=�6�   $��0q�6  
�������6*  5:      �� 5>      -   5        $#��Изменить статус��5>      $��60��6D   ���#3A79A6$��30(�6e   $��20, 20, 15, 0q�6s   $��1��6�   
�������$��Inter)�6�   #��[  6�   5?      5@      5A      