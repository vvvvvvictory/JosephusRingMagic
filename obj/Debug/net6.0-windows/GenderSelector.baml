   M S B A M L   `   `   `  ����L  HJosephusRingMagic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:JosephusRingMagicJosephusRingMagic  5   	   &     JosephusRingMagic.GenderSelector   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:JosephusRingMagic     5   	     � Title$  GenderSelector��5   	   $	��120��6    $	��400��6-    � WindowStyle$
 None=�69    � AllowsTransparency$
 True��6L   
������  � 
ResizeMode$ NoResize=�6   ��5	      �� 5
       �� 
TargetType" �6   .R�5      ��  �� CornerRadius$ 10��6   
�������
���   �$��1q�6H   ._�5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   .+�5      �� -   5        $ 	10 10 0 0��5      
�������$��30��6>   $	��Top=�6J   ._�5      �� $!��你是男孩还是女孩？��6   $��16(�6)   $��Center=�67   $��Center=�6R   
�������5      5      �� 5      $��1��6   $��8q�6*   5      5   	   5       � 	Resources 5       �� 
TargetType	 �� Property
 �� Value ��Name �� Padding �� Triggers P� Property P� Value �� 
TargetName��%  '��       �� 5      " ��6   .�5      �� 	 � ��$
 10q�6!   6.   �� 5      	 � ��$

 20 5q�6"   61   �� 5      	 � ��
 �#007ACC6%   �� 5       	 � ��

 �����6%   �� 5!      	 � ��$
 0q�6*   �� 5"      	 � ��$
 16(�6#   60   �� 5#      	 � ��
 5$   
   �� 5%      " ��6   .R�5&      � .+�5'      �� $ border��6   #��z��6'   $ 5��6Q   # z��5(      ._�5)      �� $��Center=�6#   $��Center=�6@   5*      5+       5,      P� 5-       �@J�IsMouseOver$
 True��6/   . �5.      �� $ border��6   	 � ��
 �#005CA36C   5/      P� 50       � ��$
 True��6-   . �51      �� $ border��6   	 � ��
 �#0033666C   52      54      56   	   57      58      .��59      ��  �� Orientation$ 
Horizontal=�6   $��Center=�6(   .+�5:      �� -   5        $��
男生 ♂��5:      $��Center=�6   6T   �� 5;      -   5        $��
女生 ♀��5;      $��Center=�6   6V   5<      5=      