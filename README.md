# evolutionary aproach to approximation of height maps
Aleksander Szymański
Krzysztof Sławik
## evolution
### Crossing
Crossing alowes us to change structure of map trees.
It combines two trees by randomly choosing branch from tree 'A' and randomly choosing node from tree 'B' at which branch should be pasted.
New tree coming from crossing replaces one of parents. (only if it is better)
Previously we simply added new tree to population. 
### Mutation
Mutation alowes us to tweak parameters in existing trees.
It alters parameters of given tree. (saves changes only if it improves reward)
### Randomization
Each generation adds to population random trees, which alowes to reduce homogenization of population.
### Trimming population
At this step population is being sorted and trimmed to fit population size.
### Rewarding
Reward scale is 0 to infinity, best to worst. Most of the methods were based on counting mean difference of values in the same coordinates of generated height map and the target height map.
#### ABS
Mean of absolute differences between values - something like L1 refularization i ML
#### MSE
Mean square error - count difference, raise to the second power and in the end divide by nuber of samples; L2 regularization in ML.
#### MC
Something like ABS, but instead of iterating on both maps by given step, we were picking random coordinates x and y, and checking absolute difference in drawn position. We were doing this in a loop until there was time for computation left.
#### DSSIM
Structural dissimilarity, derived from SSIM (structural similarity), used for measuring dissimilarity between images. The difference between this and previous fitness function is that DSSIM doesn't estimate absolute error, but structural information (idea that the pixels have strong inter-dependencies especially when they are spatially close).

##### RESULTS
As we could expect MC function was giving simillar scores to ABS, when given enough time, therefore we preffered to use the ABS (no need to specify time for computations).
DSSIM was very interesting approach, the scores were different from the other function scores, but it didn't make generated map more simmilar to the target map. Best results were giving MSE function, therefore we were using it in most of satisfying runs.
#### Depth penalty
We intoduced depth penalty to reduce size of map trees.
Formula: reward = reward * depthPenalty
where depthPenalty = 1 + depthPenaltyFactor * depth
## Format
Map tree consists of binnary operators and primitives (leaves).
Binnary operators: Sum(+), Product(*), Max, Min
Primitives: Perlin noise, constant, X param, Y param, elipsoid
## Results
first target
![](https://i.imgur.com/Np1LuvI.png)
result
![](https://i.imgur.com/oEHzkcM.png)
---
second target
![](https://i.imgur.com/9OPaaX9.png)
result
![](https://i.imgur.com/q7tVgsk.png)
---
third target
![](https://i.imgur.com/iZeJBkf.png)
result
![](https://i.imgur.com/9kjUEPk.png)




## GUI
### Main view
![](https://i.imgur.com/Rnb60XX.png)
**Slider** alowes changing from '*target*' to '*best so far*' view.
**Improve Evo** continues runnning evolution.
**Restart Evo** prepares running evolution.
**Parameters** brings parameters view.
**Load Map** alowes loading map as target. (any texture or map tree)
**Save Map** alowes saving '*best so far*' map tree. (xml file)
View of map is rendered with custom shader which alowes for rapid terrain mesh rebuilding.
### Parameters
![](https://i.imgur.com/hc3mTXm.png)
All parameters used by evolution can be tweaked over here.

## Summary
In current state our evolution creates models that fit quite well in general shape of provided height map. More over last changes we have introduced to our project that greatly improved our results consisted of parameter tweaking mostly. Futher parameter tweaking might still improve results.
