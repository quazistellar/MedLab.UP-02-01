   M S B A M L   `   `   `  ����C  ?MedLabUP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MedLabUPMedLabUP  5          MedLabUP.AdminMainPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:MedLabUP     5        N� Title$  LabMainPage��5	       N� 
Background �#B1DFFF.��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��200 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $	��108 �6   �� 5      $��* �6   .+�5   
   � $��0��6   $��0��6    5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5      �� -   5         ��Name$ hello��5      $��Здравствуйте,��6'   $��20(�6<   
���   �$��10q�6]   $��0��6i   $��0��6y   $
��Left=�6�   $��Center=�6�   �� 5       -   5        $ 
hello_name��5       $�� ��6,   $��20(�64   
���   �$��1��6U   $��0��6f   $��Center=�6t   5!      �� 5$   
   ���#0077DE$��2q�6'    �� CornerRadius$ 5��6;   $��1��6L   $��0��6Y   $��10q�6i   ._�5%      &   System.Windows.Controls.DataGrid  -   5          Name$ 	zakaz_fio��5%         AutoGenerateColumns .     
IsReadOnly . $��1��6X   $��0��6e   $
��Left=�6v   	   Columns	 5&      0  *System.Windows.Controls.DataGridTextColumn  5'      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Заказ (ФИО)��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *
FullName+6[     5(      $
 Статус��6)   $ * 6T    69   �� *Status+6T   5*      5+      �� 5-   
   ���#0077DE$��2q�6'   $ 5��6;   $��1��6L   $��2��6Y   $��10q�6i   ._�5.        -   5        $ analyz_dataend��5.       .   . $��1��6]   $��2��6j   $��Right=�6z   	 5/        50      $
 Анализ��6)   $ * 6X    69   �� *
AnalyzName+6X     51      $!
 Дата окончания��6)   $ * 6]    6A   �� *	DateEnd+6]   54      55      56      57      