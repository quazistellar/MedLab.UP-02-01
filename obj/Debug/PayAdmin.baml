   M S B A M L   `   `   `  ����C  ?MedLabUP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MedLabUPMedLabUP  5          MedLabUP.PayAdmin   5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:mt1http://materialdesigninxaml.net/winfx/xaml/themes  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:MedLabUP     5      $	��800��5	      $
��1500��6     N� 
Background  �#B1DFFF N� Title$ PayAdmin��5
       � 	Resources 5         RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius  RoundedComboBoxStyle d�Name	 �� 	ClickMode
 d� 	IsChecked �� Path �� Mode �� RelativeSource � ColumnDefinitions ��Name J� IsHitTestVisible F�Name �Name � AllowsTransparency ��Name-  'System.Windows.Input.KeyboardNavigation   TabNavigation �� Triggers P� Property P� Value �� 
TargetName��%�  &	        &	 �     �� 5   
   " ��6+   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $ 35��6!   #��z��63   $��0q�6]   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      5      �� 5   
   " ��6-   .�5      ��  � ��
 �����6+   �� 5       � ��$ 10q�6(   �� 5       � ��$ 15q�6'   �� 5       � �� 5      �� 5      " ��6&   .R�5       � .+�5!      �� $ 10��6%   #��z��67   $��1q�6a   ���#CCCCCC._�5"   "   d� $ ToggleButton��6/   $��0q�5#   (   
������ |�.  $	 Press=�5'   (   
 5%   (   �� $ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+.��5(   &   �  5)   *   �� 5*   .   �� 5+   .   $
��Auto �6?   .+�5-   *   �� $ PART_ContentPresenter��6K   $��0��6;    .  $��3q�6�   $
��Left=�6�   $��Center=�6�   F� 5.   *   $ arrow��6?   $��1��6/   $��0,0,10,0q�6N   ���#888"����        � 	=  	= � z     	$��Center=�6�   $��Center=�6�   5/   '   51   #   53      � 54      $ 
PART_Popup��6%   $��Bottom=�69    . |�.  $
��Fade=�6�   ��6M   �� $ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+.��55   "   �� 
�������$��1q�6<   ���#CCCCCC._�56   &   �� $ DropDownScrollViewer��63   .��57   *   �� $ Cycle=�69   58   '   59   #   5:      5;       5<      P� 5=       � {� .  . �5>   "   ��  � �� �#8886?   5?      P� 5@       �@J�IsMouseOver . . �5A   "   ��  � �� �#9996@   �� 5B   "   $ arrow��6)    � �� �#3336L   5C      5G      5I      5J      5M      .��5O      �  � RowDefinitions 5P   
   �� 5Q      $
��0.3* �6   �� 5R      $��5* �6    5U   
   �� 5V      $��4* �6   �� 5W      $��2* �6   .+�5Z   
   � $��1��6   $��1��6   $��15q�6,    5[      �� 5\      $��3* �6    �� 5]      $
��Auto �6    .+�5`      �� $��1q�6   .+�5b      �� $ 20��6   
�������
�������$��1q�6R   $��0��6f   $��50��6s   $��
15,0,15,10q�6�   ._�5c      (  "System.Windows.Controls.DatePicker  -   5          Name$ datapay_dpc��5c   !   $��0��66   $��50��6C   $��20(�6O   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Дата оплаты��6]   $��
10,0,15,10q�6~   5d      �� 5g      $ 20��6   
�������
�������$��1q�6R   $��1��6f   $��50��6s   $��
15,0,15,10q�6�   ._�5h      �� -   5         ��Name$ type_pay_cbx��5h      $��1��65   $��50��6B   $��10q�6N   $��20(�6[   $ Тип оплаты��6i   $��	0,0,15,10q�6�   5i      �� 5l      $ 20��6   
�������
�������$��1q�6R   $��2��6f   $��50��6s   $��
15,0,15,10q�6�   ._�5m      �� -   5        $ 
result_cbx��5m      $��2��63   $��50��6@   $��20(�6L   $ Результат��6Z   $��
10,0,15,10q�6y   5n      � 5u      $��1��6    5v      �� 5w      $
��Auto �6+   �� 5x      $
��Auto �6+   �� 5y      $
��Auto �6+   �� 5z      $
��Auto �6+   5~      5�      �� 5�      -   5         ��Name$ 
create_btn��5�      $!��Создать оплату��6)   ���#3A79A6
�������$��Inter)�5�   6   $��2��6I   #��[  5�      $��60��6;   $��30(�6G   $��
20,0,15,10q�6V   5�      �� 5�   
   ���#0077DE$��2q�6'   $ 5��6;   $��1��6L   $��15q�6Y   ._�5�      &   System.Windows.Controls.DataGrid  -   5          Name$ 	staffinfo��5�          AutoGenerateColumns  .  $��1��6F   !   
IsReadOnly! . "   Columns" 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn#   Header$# Дата оплаты��6)   $   Width5  /System.Windows.Controls.DataGridLengthConverter$$ * 6]   1  +System.Windows.Controls.DataGridBoundColumn%   Binding% 6>   �� *
DateCreate+6]     5�      $# Тип оплаты��6)   $$ * 6Y   % 6=   �� *	TypePay+6Y     5�      $# Результат��6)   $$ * 6W   % 6<   �� *Result+6W   5�      5�      5�      5�      