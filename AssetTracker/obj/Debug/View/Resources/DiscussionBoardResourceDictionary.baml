   M S B A M L   `   `   `  ����F  BDomainModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>.clr-namespace:DomainModel;assembly=DomainModelDomainModel  5      G CAssetTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:AssetTracker.View.PropertiesAssetTracker.View.Properties 5      K*clr-namespace:AssetTracker.View.ConvertersAssetTracker.View.Converters 5      H DMaterialIcons, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:MaterialIcons;assembly=MaterialIconsMaterialIcons 5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   NewDiscussion  �� 
TargetType �� CornerRadius  Layer2_Dark �� Padding �� Orientation  Layer1_Dark J� OpacityMask    MaterialIcons.MaterialIcon    Icon  	Text_Dark  Overlay ��Name  TextOverlay_Dark �� ElementName �� Path  IsNullToVizConverter	 �� 	Converter  HoverBtn
 �� RelativeSource �� AncestorLevel �� AncestorType  IsNullInverseConverter `� 	Converter6  0AssetTracker.View.Converters.MultiValueConverter �� Mode 	 HoverBtn_Content 
 DiscussionReply  Layer3_Dark  DiscussionItem �� DataType   DomainModel.Discussion  BackgroundColor_Dark=  7AssetTracker.View.Converters.IsNullToThicknessConverter �� Source � ColumnDefinitions �Name  TimeDisplayConv � 	Resources �� 
TargetType �� BasedOn �� Property �� Value  Key �� Triggers ^� 
Conditions �� Binding �� Value  	MatchUser �� Binding �� Value ! MatchUserInvertToVizConverter  EditBord7  1AssetTracker.View.Properties.DBOPropertyExtension   IDSource�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      8model.clr-namespace:DomainModel;assembly=DomainModel  5      3prop*clr-namespace:AssetTracker.View.Properties  5      3conv*clr-namespace:AssetTracker.View.Converters  5      9mi2clr-namespace:MaterialIcons;assembly=MaterialIcons  5      %�  &	        7  7  7  7  7  7  7  7  7  7 	 &	
 �    7  7  7  7  7  7  7  7 	 &	 �	    7  7  7  7  7  7  7  7  7 	 7  7  7  7  7  7  7 
 7  �� 5      "  ��5	      .R�5
   
   �� $��0q�6   $ 2��5      8��  $��10q�5      $
 10,0q�6   ._�5      �� $ 
Horizontal=�6   .+�5      � .+�5      �� 8�� $��4q�5      
 �����$ 40��5      $��40��5      $��40��5         5      $ ic_account_circle=�6&   $��40��5   &   $��40��5   &   8�� $��Center=�5   &   $
��0,15q�5   &   5      �� 5      $��1��6   $��10,20q�5       8�� $
 10,0q�5"      $
��1050��6)   $��Stretch=�5#      ._�5$      � .+�5%      �� $ NewDiscussionReply��6"   $��Center=�5&   "   8�� $��Stretch=�5(   "   
������ $��0q�5*   "   �� 5+      $��Start a New Discussion...��6$   8�� $
��Left=�5.   $   $��Center=�5/   $   y�5-   $   �� $ NewDiscussionReply��$
 Text�8	  5/   $   50      51      �� 53      $��1��6   $��40��54      
������ $��40��56      $��Right=�57      $��10q�58      $��0q�59      8�� ��5;      �� *$"DataContext.DiscussionReplyClicked+
 �� $ 1��$
 PageL�{�5<      �� $ NewDiscussionReply��$
 Text�8	  ��5=      `� 5>       5?        5@   "   .1�5B      �� $* $(prop:DBOPropertyExtension.IDSource)�6&   
 5C   "   �� 5D   &   $ TemplatedParent=�65   5F      �� 5G      $ NewDiscussionReply��6&   $
 Text�5H   &   5I      .��5K         $ ic_send=�6&   8��	 $��35��5L   P   5M      5N      5O      5P      �� 5R      "  ��5S      .R�5T   
   �� $��0q�6   $ 0��5U      8��  ._�5W      � .+�5Y      �� $��10,10,60,10q�6   8�� $
 10,0q�5[      ._�5\      � .+�5]      �� $ NewDiscussionReply��6"   $��Center=�5^   "   8�� $��Stretch=�5`   "   
������ $��0q�5b   "   �� 5c      $��Reply...��6$   8�� $
��Left=�5f   $   $��Center=�5g   $   y�5e   $   �� $ NewDiscussionReply��$
 Text�8	  5g   $   5h      5i      �� 5k      $��30��6   
������ $��30��5m      $��Right=�5n      $��10q�5o      $��0q�5p      8�� ��5r      �� *$"DataContext.DiscussionReplyClicked+
 �� $ 1��$
 PageL�{�5s      �� $ NewDiscussionReply��$
 Text�8	  ��5t      `� 5u       5v        5w   "   .1�5y      �� $* $(prop:DBOPropertyExtension.IDSource)�6&   
 5z   "   �� 5{   &   $ TemplatedParent=�65   5}      �� 5~      $ NewDiscussionReply��6&   $
 Text�5   &   5�      .��5�         $ ic_send=�6&   8�� 5�   &   5�      5�      5�      5�      �� 5�      "  5�      .R�5�   
   �� 8��  8�� ��5�      `� 5�       5�        5�      .1�5�      �� $ di_parentid�6   �� 5�      $ 0,0,0,50��6   5�      ��5�      `� 5�       5�        5�      .1�5�      �� $ di_parentid�6   �� 5�      $ 0,0,0,0��6   �� 5�      $ 0,4,0,0��6   5�      ._�5�      �� $ Vertical=�6   .+�5�      �� 
������ ._�5�      � $��10q�6    5�      �� 5�      $��1* �6/   $��50��5�   /   �� 5�      $	��10* �6/   .+�5�      �� 8�� $��4q�5�   "   
 �����$ 40��5�   "   $��40��5�   "   $��40��5�   "      5�      $ ic_account_circle=�6*   8�� $��40��5�   *   �� 5�      $ Vertical=�6%   $��1��5�   %   $��	10,0,0,10q�5�   %   .+�5�      � $ ReplyHeader��6#   $	��0,5q�5�   #   .+�5�   "   �� $ 
Horizontal=�6-   $
��Left=�5�   -   .+�5�   &   �� $��0,0,5,0q�5�   0   $��16(�5�   0   $��SemiBold"�5�   0   $��Bottom=�5�   0   8�� ��5�   0   �� *	User.Name+5�   0   �� 5�   &   $��Bottom=�5�   0   8�� ��5�   0   �� *	di_date+8	  5�   0   5�   #   �� 5�   "   $ 
Horizontal=�6-   $��Right=�5�   -    5�   &   ���� 5�   *   '��       " ��60   8  .�5�   .   ��  � ��$ 0,0,5,0q�5�   5   5�   +   �� 5�   *   '         "   60   8  5�   +   �� 5�   *   '��       " ��60   8 	 EditBord  5�   .   ^� 5�   2    5�   6   �� 5�   :    6D   �� $ IsMouseOver�
 �� " ��$ 1�� True5�   D   �� 5�   :    6D   �� *	di_usid+8	 
  True5�   D   .0�5�   6   ��  � y�$ Visible=�5�   =   5�   3   �� 5�   2    6>   �� *	di_usid+8	  	 False5�   >   .a�5�   6   ��  � y�$ 	Collapsed=�5�   =   5�   3   �� 5�   2    6>   �� $ IsMouseOver�
 �� " ��$ 1��	 False5�   >   .a�5�   6   ��  � y�$ 	Collapsed=�5�   =   5�   3   5�   +   5�   '   .+�5�   &   �� {�.  
������ y�5�   -   �� *	di_usid+8	  .��5�   *      $ ic_face=�6:   5�   '   �� 5�   &   8�� .��5�   *      $ ic_edit=�6:   5�   '   �� 5   &   {�.  
������ .��5  *      $ ic_more_vert=�6:   5  '   5  #   5     �� 5	     $ Vertical=�6)   .+�5
  "   �� $ DiscussionContents��6*   
������ $	��0,5q�5  *   $��0q�5  *   8�� ��5  *   �� *di_contents+5  *   5     5     5     5     �� 5     #���  5     ��5     �� *
Children+5     �� 5     8��  5     �� *di_id+y�5     �� *di_parentid+8	  5     5      5!     5"     5&     