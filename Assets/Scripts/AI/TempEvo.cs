using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Random = UnityEngine.Random;
public class TempEvo
{
    private List<TreeStructure> population = new List<TreeStructure>();
    private float endTime;
    private float bestReward;

    //initialization of population
    private void InitPopulation() {
        for(int i = 0; i < ConstParameters.populationSize; i++) {
           TreeStructure indiv = TreeStructure.MakeRandomTree(ConstParameters.maxInitDepth);
           EvaluateIndiv(indiv);
           population.Add(indiv);
        }
    }

    private void EvaluateIndiv(TreeStructure indiv) {
        HMapGen target = FileReceiver.loadedHMap;
        indiv.reward = RateMap.FitnessFunc(indiv.root, target);
    }

    private Queue<int> Sample(int n, int k) {
        Queue<int> chosen = new Queue<int>();
        for(int i = 0; i < k; i++) {
            int ind = (int)(Math.Pow(Random.Range(0f, 1f),1.5f) * n);
            chosen.Enqueue(ind);
        }
        return chosen;
    }

    //survivor selection
    private void SortAndSelectSurvivors() {
        population = population.OrderBy(r => r.reward).Take(ConstParameters.populationSize).ToList();
    }

    private void CrossPopulation()
    {
#if false
        Queue<int> parents = Sample(ConstParameters.populationSize, ConstParameters.crossings*2);
        while(parents.Count >= 2)
        {
            TreeStructure child = TreeStructure.CrossTrees(population[parents.Dequeue()],
                                                           population[parents.Dequeue()]);
            EvaluateIndiv(child);
            population.Add(child);
        }
#else
        Queue<int> parents = Sample(ConstParameters.populationSize, ConstParameters.crossings*2);
        while (parents.Count >= 2)
        {
            TreeStructure parent1 = population[parents.Dequeue()];
            TreeStructure parent2 = population[parents.Dequeue()];
            TreeStructure child = TreeStructure.CrossTrees(parent1, parent2);
            EvaluateIndiv(child);
            if(child.reward < parent1.reward)
            {
                parent1.root = child.root;
                parent1.reward = child.reward;
                continue;
            }
            if (child.reward < parent2.reward)
            {
                parent2.root = child.root;
                parent2.reward = child.reward;
                continue;
            }
        }
#endif
    }
    private void MutatePopulation()
    {
#if false
        List<TreeStructure> mutated = new List<TreeStructure>();
        for(int i = 0; i < ConstParameters.mutations && i < ConstParameters.populationSize; i++)
        {
            var mutatedTree = TreeStructure.Mutate(population[i]);
            EvaluateIndiv(mutatedTree);
            mutated.Add(mutatedTree);
        }
        population.AddRange(mutated);
#else
        for (int i = 0; i < ConstParameters.mutations && i < ConstParameters.populationSize; i++)
        {
            var mutatedTree = TreeStructure.Mutate(population[i]);
            EvaluateIndiv(mutatedTree);
            if(population[i].reward > mutatedTree.reward)
            {
                population[i].root = mutatedTree.root;
                population[i].reward = mutatedTree.reward;
            }
        }
#endif
    }
    private void AddRandomPopulation()
    {
        for (int i = 0; i < ConstParameters.randoms; i++)
        {
            TreeStructure indiv = TreeStructure.MakeRandomTree(ConstParameters.maxInitDepth);
            EvaluateIndiv(indiv);
            population.Add(indiv);
        }
    }
    public TreeStructure Evolution() {
        InitPopulation();
        SortAndSelectSurvivors();
        return population[0];
    }
    public TreeStructure ImprovePopulation(float time)
    {
        List<Func<bool>> endConditions = new List<Func<bool>>();
        if (ConstParameters.useTime)
        {
            endConditions.Add(TimeEndCondition);
            endTime = Time.realtimeSinceStartup + time;
        }
        if (ConstParameters.useError)
        {
            endConditions.Add(RewardEndCondition);
            bestReward = Mathf.Infinity;
        }
        while (!OrMap(endConditions))
        {
            CrossPopulation();
            MutatePopulation();
            AddRandomPopulation();
            SortAndSelectSurvivors();
            bestReward = population[0].reward;
        }
        return population[0];
    }
    bool TimeEndCondition()
    {
        return endTime < Time.realtimeSinceStartup;
    }
    bool RewardEndCondition()
    {
        return bestReward < ConstParameters.errorThreshold;
    }
    bool OrMap(List<Func<bool>> conditions)
    {
        foreach(var cond in conditions)
        {
            if (cond.Invoke()) return true;
        }
        return false;
    }
}
