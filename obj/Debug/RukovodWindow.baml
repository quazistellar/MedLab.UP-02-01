   M S B A M L   `   `   `  ����C  ?MedLabUP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MedLabUPMedLabUP  5   	       MedLabUP.RukovodWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:MedLabUP     5   	     � Title$  Руководитель��5   	   $	��800��6   $
��1500��6+    � Icon$ iconmedlab.PNG��68    � WindowStartupLocation$ CenterScreen=�5	   	   ���#B1DFFF � WindowState$ 	Maximized=�6C   .��5
      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      .+�5   
   � $��0��6   ���#8DB8C4 � ColumnDefinitions 5      �� 5      $��7 �6#   �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $
��Auto �6#   �� 5      $��18 �6#   .+�5      �� -   5         ��Name$ show_otcheti��5      ���#B9E1ED$��40��6@   $��1��6L   ���#7C9FA9$��1q�6r   $��Отчёты��6�   
���   �6�   �� 5      -   5        $ show_analyzis��5      $��2��6,   $��40��6<   ���#B9E1ED���#7C9FA9$��1q�6s   $��Анализы��6�   
���   �6�   �� 5      -   5        $ 	show_main��5      $��3��6(   $��40��68   ���#B9E1ED���#7C9FA9$��1q�6o   $��Главная��6�   
���   �6�   �� 5       -   5        $ 
show_staff��5       $��4��6)   $��40��69   ���#B9E1ED���#7C9FA9$��1q�6p   $��Сотрудники��6�   
���   �6�   �� 5!      -   5        $
 exit��5!      $��5��6#   $��41��63   $��40��6>   ���#B9E1ED$��×��6_   
���   �$��15(�6~   ���#7C9FA9$��1q�6�   6�   5$       � 5&   
   -   5          �Name$
 page��5&      $��1��6     � NavigationUIVisibility$ Hidden=�6+   6M   5'      5(      