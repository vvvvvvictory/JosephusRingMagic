   M S B A M L   `   `   `  ���� L  HJosephusRingMagic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:JosephusRingMagicJosephusRingMagic  &     JosephusRingMagic.GenderSelector   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:JosephusRingMagic       � Title$  GenderSelector��$	��120��$	��400�� � WindowStyle$
 None=� � AllowsTransparency$
 True��
������  � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterOwner=�����  �� 
TargetType" �.R���  �� CornerRadius$ 10��
�������
���   �$��1q�._��  � RowDefinitions �� $
��Auto ��� $��* �.+��� -   $ 	10 10 0 0��
�������$��30��$	��Top=�._��� $!��你是男孩还是女孩？��$��16(�$��Center=�$��Center=�
��������� $��1��$��8q� � 	Resources 	 �� 
TargetType
 �� Property �� Value ��Name �� Padding �� Triggers P� Property P� Value �� 
TargetName��%�  '��       �� "	 ��.��� 
 � ��$ 10q��� 
 � ��$
 20 5q��� 
 � �� �#007ACC�� 
 � ��
 ������� 
 � ��$ 0q��� 
 � ��$ 16(��� 
 � �� �� " ��.R�� .+��� $ border��#��z��$ 5��# z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ��� $ border��
 � �� �#005CA3P�  � ��$
 True��. ��� $ border��
 � �� �#003366.����  �� Orientation$ 
Horizontal=�$��Center=�.+��� -   $��
男生 ♂��$��Center=��� -   $��
女生 ♀��$��Center=�