   M S B A M L   `   `   `  ����C  ?MedLabUP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MedLabUPMedLabUP  5          MedLabUP.AdminClientsPage   5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:mt1http://materialdesigninxaml.net/winfx/xaml/themes  5      FmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:MedLabUP     5        N� 
Background  �#B1DFFF N� Title$ AdminClientsPage��5       � 	Resources 5         RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius��%�   &	        �� 5   
   " ��6+   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $ 35��6!   #��z��63   $��0q�6]   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5       �� 5!      $��25 �6   	 � ColumnDefinitions	 5#   
   �� 5$      $��15 �6   �� 5%      $
��1080 �6   �� 5&      $��* �6   .+�5)   
   �� ���#0077DE$��2q�6'   $ 5��6;   $��1��6L   $��1��6Y   ._�5*      &   System.Windows.Controls.DataGrid  -   5        
  Name$
 analyzes��5*         AutoGenerateColumns .  $��1��6E   $��1q�6S   $��1��6g      
IsReadOnly .    Columns 5+      0  *System.Windows.Controls.DataGridTextColumn  5,      ,  &System.Windows.Controls.DataGridColumn   Header$ Пациент (ФИО)��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6c   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   �� *ClientFullName+6c     5-      $' !Контактные данные��6)   $ * 6d    6D   �� *EmailClient+6d     5.      $- 'Наименование анализа��6)   $ * 6f    6G   �� *
AnalyzName+6f   50      51      � 53   
   $��2��6   $��1��6    54      �� 55      $��* �6    �� 56      $
��Auto �6    .+�59      �� $ 30��6   
�������$��60��6:   $��	20,0,15,0q�6G    �� Padding$ 10q�6Z   $��0��6g   $	��Top=�6t   $��Stretch=�6�   ._�5:      �� -   5         ��Name$ 
filter_cbx��5:      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Тип анализа��6/   $��20(�6P   $��Inter)�6^   
���   �
�������$��0,7,0,0q�6�   $	��Top=�6�   $��0q�6�   
�������6�   5;      5=      5>      5@      