   M S B A M L   `   `   `  ����C  ?MedLabUP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MedLabUPMedLabUP  5   	       MedLabUP.MainWindow   5   	   _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:mt1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:MedLabUP     5   	   FmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Авторизация��5
   	   $	��272��6   $	��400��6*   ���#B1DFFF � WindowStartupLocation$ CenterScreen=�5   	    � Icon$ iconmedlab.png��6.    � 
ResizeMode$ NoResize=�6D    � 	Resources 5         RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType	 �� CornerRadius  MaterialDesignTextBoxBase
 �� BasedOn  TextBoxNoUnderline.  (MaterialDesignThemes.Wpf.TextFieldAssist  MaterialDesignPasswordBox  PasswordBoxNoUnderline��%�  &	        &	 �     7  &	 E    7  �� 5   
   " ��6+   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $	 20��6!   #��z��63   $��0q�6]   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      5      �� 5   
   " ��6   8
   .�5      ��  �@ UnderlineBrush
 ���� 6N   5      �� 5   
   " G�6   8
   .�5      ��  �@ UnderlineBrush
 ���� 6N   5       5"      .��5%      �  � RowDefinitions 5&   
   �� 5'      $
��Auto �6   �� 5(      $��32 �6   �� 5)      $
��Auto �6   �� 5*      $��* �6   �� 5+      $
��Auto �6   �� 5,      $��* �6   �� 5-      $
��Auto �6   �� 5.      $��30 �6   �� 5/      $��* �6   .+�52   
   � $��0��6   ���#8DB8C4$��31��61   .+�54      �� $��МедЛаб��6   $��0��6&   $��
Andada Pro)�63   $��Center=�6K   $��Right=�6f   ���#2C3B40 �� Padding$ 0,6,6,8q�6�    �� TextAlignment$ Center=�6�   6�   56      �� 58   
   $��АВТОРИЗАЦИЯ��6   $��1��6'   ���#000122$��Center=�6I   $��14(�6f   $��Inter)�6t   $��Center=�6�   �� 5:   
   $	 20��6   
�������
�������$��1q�6J   $��2��6^   $	��303��6l   $��36��6x   ._�5;      �� -   5         ��Name$ LoginUser_tbx��5;      $	��303��6.   $��36��6:   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 
Логин��6F   $��2��6a   
������ $��5,0,5,0q�6�   $��0q�6�   #��[ 6�   5<      �� 5?   
   $	 20��6   
�������
�������$��1q�6J   $��4��6^   $	��303��6l   $��36��6x   ._�5@      G� -   5         G�Name$ Password_tbx��5@      $	��303��61   $��36��6=   $ Пароль��6I   $��4��6f   
������ $��5,0,5,0q�6�   $��0q�6�   #��[ 6�   5A      �� 5C   
   -   5         ��Name$ Autorize_btn��5C      $��
Войти��6'   ���#3A79A6
�������$��Inter)�6{   $��6��6�   #��[  6�   $��29��6�   $��Center=�6�   $��134,0,134,0q�6�   �� 5D   
   -   5        $ exit_btn��5D      $��
Выход��6#   ���#3A79A6
�������$��Inter)�6s   $��7��6�   #��[  6�   $��144,5,144,0q�6�   5F      5G      