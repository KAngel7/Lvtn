   M S B A M L   `   `   `  ����
  TTTN-1    TTTN_1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Neuron Network��5   	   $	��586��6    $	��619��6-    � 
ResizeMode$ CanMinimize=�69   
 � Top$ 10��6R    � Left$	 300��6[   .��5      � .+�5   
   �� -   5        $��	groupBox1��5   f   $��Config network��6   $	��137��6+   $
��Left=�68   $��	25,12,0,0q�6S   $	��Top=�6w   $	��539��6�   .��5      � $��99��6   $	��521��6   .+�5      �� -   5        $��label1��5   g   $��
inputNodes��6   $��28��6-   $
��Left=�69   $��	26,17,0,0q�6T   $	��Top=�6u   �� 5	      -   5        $��n_inputNodes��5	   U   $��23��6   $
��Left=�6&   $��
104,17,0,0q�6A   $	��Top=�6i   $��46��6�   6�   �� 5
      -   5        $��label2��5
   h   $��
hidenNodes��6   $��28��6-   $
��Left=�69   $��
181,16,0,0q�6T   $	��Top=�6v   �� 5      -   5        $��n_hidenNodes��5   U   $��23��6   $
��Left=�6&   $��
266,17,0,0q�6A   $	��Top=�6i   $��53��6�   �� 5      -   5        $��label3��5   i   $��outputNodes��6   $��28��6.   $
��Left=�6:   $��
354,16,0,0q�6U   $	��Top=�6w   �� 5      -   5        $��n_outputNodes��5   U   $��23��6   $
��Left=�6&   $��
440,22,0,0q�6A   $	��Top=�6j   $��57��6�   �� 5      -   5        $��btn_cfg_create��5   e   $��Create��6   $��23��6*   $
��Left=�66   $��
422,62,0,0q�6Q   $	��Top=�6{   $��75��6�   6�   �� 5      -	   5        $��btn_cfg_reset��5   d   $��Reset��6   $��23��6)   $
��Left=�65   $��
325,62,0,0q�6P   $	��Top=�6y   $��75��6�   6�   5      5      �� 5   
   -
   5        $��
G_training��5   a   $��Training��6   $	��244��6%   $
��Left=�62   $��
25,163,0,0q�6M   $	��Top=�6s   $	��539��6�   .��5      � .+�5      �� -   5        $��label4��5   _   $
��Data��6   $��28��6'   $
��Left=�63   $��6,6,0,0q�6N   $	��Top=�6m   �� 5      -   5        $��datafilebox��5   T   $��23��6   $
��Left=�6&   $��	55,10,0,0q�6A   $	��Top=�6g   $	��382��6   �� 5      -   5        $��btn_trn_browse��5   d   $��Browse��6   $��23��6*   $��Right=�66   $��0,10,6,0q�6R   $	��Top=�6z   $��75��6�   6�   �� 5      -   5        $��btn_trn_graph��5   e   $��Graph��6   $��23��6)   $��Right=�65   $��
0,116,13,0q�6Q   $	��Top=�6z   $��75��6�   6�   �� 5      -   5        $	��lbl��5   l   $��Total data row:��6   $��28��62   $
��Left=�6>   $��	55,39,0,0q�6Y   $	��Top=�6w   �� 5      -   5        $��label6��5   k   $��Train from row��6   $��28��61   $
��Left=�6=   $��	96,67,0,0q�6X   $	��Top=�6y   �� 5      -   5        $��label7��5   a   $��To��6   $��28��6%   $��Right=�61   $��
0,68,186,0q�6M   $	��Top=�6o   �� 5      -   5        $��trn_from��5   U   $��23��6   $
��Left=�6&   $��
198,67,0,0q�6A   $	��Top=�6e   $��90��6}   �� 5      -   5        $��trn_to��5   U   $��23��6   $
��Left=�6&   $��
347,70,0,0q�6A   $	��Top=�6c   $��90��6{   	� 5      -   5        $��radio_bp��5   t   $��Back-Propagation��6   $��16��69   $
��Left=�6E   $��
96,150,0,0q�6`   $	��Top=�6�   	� 5      -   5        $��radio_sa��5   y   $��Simulated Annealing��6   $��16��6<   $��Right=�6H   $��0,150,173,0q�6d   $	��Top=�6�   �� 5      -   5        $��label8��5   n   $��Select Algorithm��6   $��28��63   $
��Left=�6?   $��
55,116,0,0q�6Z   $	��Top=�6|   �� 5       -   5        $��btn_trn_train��5    h   $��Training��6   $��23��6,   $
��Left=�68   $��425,182,0,0q�6S   $	��Top=�6}   $��75��6�   6�   �� 5!      -   5        $��btn_trn_reset��5!   e   $��Reset��6   $��23��6)   $
��Left=�65   $��328,182,0,0q�6P   $	��Top=�6z   $��75��6�   �� 5"      -   5        $��totalrow��5"   V   $��23��6   $��Right=�6&   $��
0,39,298,0q�6B   $	��Top=�6f   $��80��6~   	� 5#      -   5        $��
radio_sa_2��5#   x   $��Simulated Annealing��6   $��16��6<   $��Right=�6H   $��
0,150,27,0q�6d   $	��Top=�6�   $y�Hidden=�6�   5$      5%      �� 5&   
   -   5        $��	groupBox3��5&   `   $��Testing��6   $	��110��6$   $
��Left=�61   $��
25,421,0,0q�6L   $	��Top=�6q   $	��539��6�   .��5'      � .+�5(      �� -   5        $��label9��5(   j   $��Test from row��6   $��28��60   $
��Left=�6<   $��	37,17,0,0q�6W   $	��Top=�6x   �� 5)      -   5        $��label10��5)   f   $��To row ��6   $��28��6*   $��Right=�66   $��
0,17,209,0q�6R   $	��Top=�6u   �� 5*      -   5        $��	test_from��5*   U   $��23��6   $
��Left=�6&   $��
139,17,0,0q�6A   $	��Top=�6f   $��90��6~   �� 5+      -   5        $��test_to��5+   U   $��23��6   $
��Left=�6&   $��
347,17,0,0q�6A   $	��Top=�6d   $��90��6|   �� 5,      -    5        $��btn_test��5,   W   $
��Test��6   $
��Left=�6(   $��
425,58,0,7q�6C   $��75��6g   6r   �� 5-      -!   5        $��btn_test_reset��5-   d   $��Reset��6   $��23��6)   $
��Left=�65   $��
328,58,0,0q�6P   $	��Top=�6z   $��75��6�   6�   5.      5/      �� 50   
   -"   5        $��	groupBox4��50   H   $��Forecasting��6   $��72��6(   $��
0,554,46,0q�64   $	��Top=�6Y   $��Right=�6q   $	��539��6�   $y�Hidden=�6�   .��51      � .+�52      �� -#   5        $��
n_forecast��52   T   $��23��6   $
��Left=�6&   $��	149,6,0,0q�6A   $	��Top=�6f   $	��120��6~   �� 53      -$   5        $��label11��53   k   $��Number of row: ��6   $��28��62   $
��Left=�6>   $��46,6,0,0q�6Y   $	��Top=�6z   �� 54      -%   5        $��btn_fc_forecast��54   j   $��Forecasting��6   $��23��6/   $
��Left=�6;   $��
425,20,0,0q�6V   $	��Top=�6�   $��75��6�   6�   �� 55      -&   5        $��btn_fc_reset��55   d   $��Reset��6   $��23��6)   $
��Left=�65   $��
328,20,0,0q�6P   $	��Top=�6x   $��75��6�   6�   56      57      58      59      