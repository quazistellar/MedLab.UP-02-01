   M S B A M L   `   `   `  ����C  ?MedLabUP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MedLabUPMedLabUP  5   	       MedLabUP.EmailDialog   5   	   _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:mt1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:MedLabUP     5   	     � Title$=  7Отправка по электронной почте��5	      $	��250��6-   $	��300��6:    � WindowStartupLocation$ CenterScreen=�5
   	   ���#B1DFFF � 
ResizeMode$ NoResize=�6C    � Icon$ iconmedlab.PNG��6Y    � 	Resources 5         RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType	 �� CornerRadius��%�   &	        �� 5   
   " ��6+   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $	 15��6!   #��z��63   $��0q�6]   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      5      5      .��5      � $��10q�6   
 � RowDefinitions
 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5       $��* �6   �� 5!      $
��Auto �6   �� 5"      $
��Auto �6   .+�5%   
   �� $	 20��6   
�������
�������$��1q�6J   $��1��6^   $��40��6k   ._�5&      �� -   5         ��Name$ txtPassword��5&   #   $��1��6   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Пароль��69   $��0,2,0,5q�6U   $��30��6f   $	��260��6r   5'      �� 5+   
   $	 20��6   
�������
�������$��1q�6J   $��2��6^   $��40��6k   ._�5,      �� -   5        $ toemail��5,   #   $��2��6   $ Email пациента��65   $��0,2,0,5q�6Y   $��30��6j   $	��260��6v   5-      �� 51   
   $	 20��6   
�������
�������$��1q�6J   $��0��6^   $��40��6k   ._�52      �� -   5        $ txtFromEmail��52   #   $��0��6   $" Email отправителя��69   $��0,2,0,5q�6`   $	��260��6q   53      �� 56   
   -   5         ��Name$ 
create_btn��56      $%��Отправить письмо��6%   ���#3A79A6
�������$��Inter)�57   /   $��3��6B   #��[  58      $��30��63   $��0,10,0,0q�6@   �� 5:   
   -   5        $
 exit��5:      $��
Выход��6   ���#3A79A6
�������$��Inter)�5;   *   $��5��6=   #��[  5<      $��30��6.   $��0,10,0,0q�6;   5>      5?      