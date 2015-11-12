﻿using System;
using FannWrapperDouble;
using FannWrapper;
using System.Collections.Generic;

namespace FANNCSharp
{
    public class NeuralNetDouble : IDisposable
    {
        neural_net net = null;

        public NeuralNetDouble(NeuralNetDouble other)
        {
           net = new neural_net(other.InternalFloatNet);
        }

        public void Dispose()
        {
           net.destroy();
        }

        public NeuralNetDouble(NetworkType netType, uint numLayers, params uint[]args)
        {
            using (uintArray newLayers = new uintArray((int)numLayers))
            {
                for (int i = 0; i < args.Length; i++)
                {
                    newLayers.setitem(i, args[i]);
                }
                Outputs = args[args.Length - 1];
                net = new neural_net(netType, numLayers, newLayers.cast());
            }
        }

        public NeuralNetDouble(NetworkType netType, ICollection<uint> layers)
        {
            using (uintArray newLayers = new uintArray(layers.Count))
            {
                IEnumerator<uint> enumerator = layers.GetEnumerator();
                int i = 0;
                do
                {
                    newLayers.setitem(i, enumerator.Current);
                    i++;
                } while (enumerator.MoveNext());
                Outputs = newLayers.getitem(layers.Count - 1);
                net = new neural_net(netType, (uint)layers.Count, newLayers.cast());
            }
        }

        public NeuralNetDouble(float connectionRate, uint numLayers, params uint[] args)
        {
            using (uintArray newLayers = new uintArray((int)numLayers))
            {
                for (int i = 0; i < args.Length; i++)
                {
                    newLayers.setitem(i, args[i]);
                }
                Outputs = args[args.Length - 1];
                net = new neural_net(connectionRate, numLayers, newLayers.cast());
            }
        }

        public NeuralNetDouble(float connectionRate, ICollection<uint> layers)
        {
            using (uintArray newLayers = new uintArray(layers.Count))
            {
                IEnumerator<uint> enumerator = layers.GetEnumerator();
                int i = 0;
                do
                {
                    newLayers.setitem(i, enumerator.Current);
                    i++;
                } while (enumerator.MoveNext());
                Outputs = newLayers.getitem(layers.Count - 1);
                net = new neural_net(connectionRate, (uint)layers.Count, newLayers.cast());
            }
        }

        public NeuralNetDouble(string filename)
        {
            net = new neural_net(filename);
        }

        public double[] Run(double[] input)
        {
            using (doubleArray doubles = new doubleArray(input.Length))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    doubles.setitem(i, input[i]);
                }
                using (doubleArray outputs = doubleArray.frompointer(net.run(doubles.cast())))
                {
                    double[] result = new double[Outputs];
                    for (int i = 0; i < Outputs; i++)
                    {
                        result[i] = outputs.getitem(i);
                    }
                    return result;
                }
            }
        }

        public void RandomizeWeights(double minWeight, double maxWeight)
        {
           net.randomize_weights(minWeight, maxWeight);
        }
        public void InitWeights(TrainingDataDouble data)
        {
           net.init_weights(data.InternalData);
        }

        public void PrintConnections()
        {
           net.print_connections();
        }
        
        public bool Save(string file)
        {
            return net.save(file);
        }

        public int SaveToFixed(string file)
        {
            return net.save_to_fixed(file);
        }

        public void Train(double[] input, double[] desiredOutput)
        {
            using (doubleArray doublesIn = new doubleArray(input.Length))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    doublesIn.setitem(i, input[i]);
                }
                doubleArray doublesOut = new doubleArray(desiredOutput.Length);
                for (int i = 0; i < input.Length; i++)
                {
                    doublesOut.setitem(i, input[i]);
                }
               net.train(doublesIn.cast(), doublesOut.cast());
            }
        }

        public float TrainEpoch(TrainingDataDouble data)
        {
            return net.train_epoch(data.InternalData);
        }

        public void TrainOnData(TrainingDataDouble data, uint maxEpochs, uint epochsBetweenReports, float desiredError)
        {
           net.train_on_data(data.InternalData, maxEpochs, epochsBetweenReports, desiredError);
        }

        public void TrainOnFile(string filename, uint maxEpochs, uint epochsBetweenReports, float desiredError)
        {
           net.train_on_file(filename, maxEpochs, epochsBetweenReports, desiredError);
        }

        public double[] Test(double[] input, double[] desiredOutput)
        {
            using (doubleArray doublesIn = new doubleArray(input.Length))
            using (doubleArray doublesOut = new doubleArray(desiredOutput.Length))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    doublesIn.setitem(i, input[i]);
                }
                for (int i = 0; i < desiredOutput.Length; i++)
                {
                    doublesOut.setitem(i, desiredOutput[i]);
                }
                doubleArray result = doubleArray.frompointer(net.test(doublesIn.cast(), doublesOut.cast()));
                double[] arrayResult = new double[Outputs];
                for (int i = 0; i < Outputs; i++)
                {
                    arrayResult[i] = result.getitem(i);
                }
                return arrayResult;
            }
        }

        public float TestData(TrainingDataDouble data)
        {
            return net.test_data(data.InternalData);
        }

        public float MSE
        {
            get
            {
                return net.get_MSE();
            }
        }

        public void ResetMSE()
        {
           net.reset_MSE();
        }

        public void PrintParameters()
        {
           net.print_parameters();
        }

        public TrainingAlgorithm TrainingAlgorithm
        {
            get
            {
                return net.get_training_algorithm();
            }
            set
            {
                net.set_training_algorithm(value);
            }
        }

        public float LearningRate
        {
            get
            {
                return net.get_learning_rate();
            }
            set
            {
                net.set_learning_rate(value);
            }
        }
        public ActivationFunction GetActivationFunction(int layer, int neuron)
        {
            return net.get_activation_function(layer, neuron);
        }

        public void SetActivationFunction(ActivationFunction function, int layer, int neuron)
        {
           net.set_activation_function(function, layer, neuron);
        }

        public void SetActivationFunctionLayer(ActivationFunction function, int layer)
        {
           net.set_activation_function_layer(function, layer);
        }

        public ActivationFunction ActivationFunctionHidden
        {
            set
            {
                net.set_activation_function_hidden(value);
            }
        }

        public ActivationFunction ActivationFunctionOutput
        {
            set
            {
                net.set_activation_function_output(value);
            }
        }

        public double GetActivationSteepness(int layer, int neuron)
        {
            return net.get_activation_steepness(layer, neuron);
        }

        public void SetActivationSteepness(double steepness, int layer, int neuron)
        {
           net.set_activation_steepness(steepness, layer, neuron);
        }

        public void SetActivationSteepnessLayer(double steepness, int layer)
        {
           net.set_activation_steepness_layer(steepness, layer);
        }

        public void SetActivationSteepnessHidden(double steepness)
        {
           net.set_activation_steepness_hidden(steepness);
        }

        public void SetActivationSteepnessOutput(double steepness)
        {
           net.set_activation_steepness_output(steepness);
        }

        public ErrorFunction TrainErrorFunction
        {
            get
            {
                return net.get_train_error_function();
            }
            set
            {
                net.set_train_error_function(value);
            }
        }

        public float QuickpropDecay
        {
            get
            {
                return net.get_quickprop_decay();
            }
            set
            {
                net.set_quickprop_decay(value);
            }
        }

        public float QuickpropMu
        {
            get
            {
                return net.get_quickprop_mu();
            }
            set
            {
                net.set_quickprop_mu(value);
            }
        }
        public float RpropIncreaseFactor
        {
            get
            {
                return net.get_rprop_increase_factor();
            }
            set
            {
                net.set_rprop_increase_factor(value);
            }
        }
        public float RpropDecreaseFactor
        {
            get
            {
                return net.get_rprop_decrease_factor();
            }
            set
            {
                net.set_rprop_decrease_factor(value);
            }
        }
        public float RpropDeltaZero
        {
            get
            {
                return net.get_rprop_delta_zero();
            }
            set
            {
                net.set_rprop_delta_zero(value);
            }
        }
        public float RpropDeltaMin
        {
            get
            {
                return net.get_rprop_delta_min();
            }
            set
            {
                net.set_rprop_delta_min(value);
            }
        }
        public float RpropDeltaMax
        {
            get
            {
                return net.get_rprop_delta_max();
            }
            set
            {
                net.set_rprop_delta_max(value);
            }
        }
        public float SarpropWeightDecayShift
        {
            get
            {
                return net.get_sarprop_weight_decay_shift();
            }
            set
            {
                net.set_sarprop_weight_decay_shift(value);
            }
        }
        public float SarpropStepErrorThresholdFactor
        {
            get
            {
                return net.get_sarprop_step_error_threshold_factor();
            }
            set
            {
                net.set_sarprop_step_error_threshold_factor(value);
            }
        }
        public float SarpropStepErrorShift
        {
            get
            {
                return net.get_sarprop_step_error_shift();
            }
            set
            {
                net.set_sarprop_step_error_shift(value);
            }
        }
        public float SarpropTemperature
        {
            get
            {
                return net.get_sarprop_temperature();
            }
            set
            {
                net.set_sarprop_temperature(value);
            }
        }
        
        public uint InputCount
        {
            get
            {
                return net.get_num_input();
            }
        }
        public uint OutputCount
        {
            get
            {
                return net.get_num_output();
            }
        }
        public uint TotalNeurons
        {
            get
            {
                return net.get_total_neurons();
            }
        }
        public uint TotalConnections
        {
            get
            {
                return net.get_total_connections();
            }
        }
        public NetworkType NetworkType
        {
            get
            {
                return net.get_network_type();
            }
        }
        public float ConnectionRate
        {
            get
            {
                return net.get_connection_rate();
            }
        }
        public uint LayerCount
        {
            get
            {
                return net.get_num_layers();
            }
        }
        public uint[] LayerArray
        {
            get
            {
                uint[] layers = new uint[net.get_num_layers()];
                using (uintArray array = new uintArray(layers.Length))
                {
                    net.get_layer_array(array.cast());
                    for (int i = 0; i < layers.Length; i++)
                    {
                        layers[i] = array.getitem(i);
                    }
                }
                return layers;
            }
        }
        public uint[] BiasArray
        {
            get
            {
                uint[] bias = new uint[net.get_num_layers()];
                using (uintArray array = new uintArray(bias.Length))
                {
                    net.get_layer_array(array.cast());
                    for (int i = 0; i < bias.Length; i++)
                    {
                        bias[i] = array.getitem(i);
                    }
                }
                return bias;
            }
        }
        public Connection[] ConnectionArray
        {
            get {
                uint count = net.get_total_connections();
                Connection[] connections = new Connection[count];
                using (ConnectionArray output = new ConnectionArray(connections.Length))
                {
                   net.get_connection_array(output.cast());
                    for (uint i = 0; i < count; i++)
                    {
                        connections[i] = output.getitem((int)i);
                    }
                }
                return connections;
            }
        }
        public Connection[] WeightArray
        {
            set
            {
                using (ConnectionArray input = new ConnectionArray(value.Length))
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        input.setitem(i, value[i]);
                    }
                    net.set_weight_array(input.cast(), (uint)value.Length);
                }
            }
        }
        public void SetWeight(uint from_neuron, uint to_neuron, double weight)
        {
           net.set_weight(from_neuron, to_neuron, weight);
        }
        public float LearningMomentum
        {
            get
            {
                return net.get_learning_momentum();
            }
            set
            {
                net.set_learning_momentum(value);
            }
        }
        public StopFunction TrainStopFunction
        {
            get
            {
                return net.get_train_stop_function();
            }
            set
            {
                net.set_train_stop_function(value);
            }
        }
        public double BitFailLimit
        {
            get
            {
                return net.get_bit_fail_limit();
            }
            set
            {
                net.set_bit_fail_limit(value);
            }
        }
        public uint BitFail
        {
            get
            {
                return net.get_bit_fail();
            }
        }
        public void CascadetrainOnData(TrainingDataDouble data, uint max_neurons, uint neurons_between_reports, float desired_error)
        {
           net.cascadetrain_on_data(data.InternalData, max_neurons, neurons_between_reports, desired_error);
        }
        public void CascadetrainOnFile(string filename, uint max_neurons, uint neurons_between_reports, float desired_error)
        {
           net.cascadetrain_on_file(filename, max_neurons, neurons_between_reports, desired_error);
        }
        public float CascadeOutputChangeFraction
        {
            get
            {
                return net.get_cascade_output_change_fraction();
            }
            set
            {
                net.set_cascade_output_change_fraction(value);
            }
        }
        public uint CascadeOutputStagnationEpochs
        {
            get
            {
                return net.get_cascade_output_stagnation_epochs();
            }
            set
            {
                net.set_cascade_output_stagnation_epochs(value);
            }
        }
        public float CascadeCandidateChangeFraction
        {
            get
            {
                return net.get_cascade_candidate_change_fraction();
            }
            set
            {
                net.set_cascade_output_change_fraction(value);
            }
        }
        public uint CascadeCandidateStagnationEpochs
        {
            get
            {
                return net.get_cascade_candidate_stagnation_epochs();
            }
            set
            {
                net.set_cascade_candidate_stagnation_epochs(value);
            }
        }
        public double CascadeWeightMultiplier
        {
            get
            {
                return net.get_cascade_weight_multiplier();
            }
            set
            {
                net.set_cascade_weight_multiplier(value);
            }
        }
        public double CascadeCandidateLimit
        {
            get
            {
                return net.get_cascade_candidate_limit();
            }
            set
            {
                net.set_cascade_candidate_limit(value);
            }
        }
        public uint CascadeMaxOutEpochs
        {
            get
            {
                return net.get_cascade_max_out_epochs();
            }
            set
            {
                net.set_cascade_max_out_epochs(value);
            }
        }
        public uint CascadeMaxCandEpochs
        {
            get
            {
                return net.get_cascade_max_cand_epochs();
            }
            set
            {
                net.set_cascade_max_cand_epochs(value);
            }
        }
        public uint CascadeCandidatesCount
        {
            get
            {
                return net.get_cascade_num_candidates();
            }
        }
        public uint CascadeActivationFunctionsCount
        {
            get
            {
                return net.get_cascade_activation_functions_count();
            }
        }
        public ActivationFunction[] CascadeActivationFunctions
        {
            get
            {
                int count = (int)net.get_cascade_activation_functions_count();
                using (ActivationFunctionArray result = ActivationFunctionArray.frompointer(net.get_cascade_activation_functions()))
                {
                    ActivationFunction[] arrayResult = new ActivationFunction[net.get_cascade_activation_functions_count()];
                    for (int i = 0; i < count; i++)
                    {
                        arrayResult[i] = result.getitem(i);
                    }
                    return arrayResult;
                }
            }
            set
            {
                using (ActivationFunctionArray input = new ActivationFunctionArray(value.Length))
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        input.setitem(i, value[i]);
                    }
                    net.set_cascade_activation_functions(input.cast(), (uint)value.Length);
                }
            }
        }
        public uint CascadeActivationSteepnessesCount
        {
            get
            {
                return net.get_cascade_activation_steepnesses_count();
            }
        }
        public double[] CascadeActivationSteepnesses
        {
            get
            {
                using (doubleArray result = doubleArray.frompointer(net.get_cascade_activation_steepnesses()))
                {
                    uint count = net.get_cascade_activation_steepnesses_count();
                    double[] resultArray = new double[net.get_cascade_activation_steepnesses_count()];
                    for (int i = 0; i < count; i++)
                    {
                        resultArray[i] = result.getitem(i);
                    }
                    return resultArray;
                }
            }
            set
            {
                using (doubleArray input = new doubleArray(value.Length))
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        input.setitem(i, value[i]);
                    }
                    net.set_cascade_activation_steepnesses(input.cast(), (uint)value.Length);
                    for (int i = 0; i < value.Length; i++)
                    {
                        value[i] = input.getitem(i);
                    }
                }
            }
        }
        public uint CascadeCandidateGroupsCount
        {
            get
            {
                return net.get_cascade_num_candidate_groups();
            }
            set
            {
                net.set_cascade_num_candidate_groups(value);
            }
        }
        public void ScaleTrain(TrainingDataDouble data)
        {
           net.scale_train(data.InternalData);
        }
        public void DescaleTrain(TrainingDataDouble data)
        {
           net.descale_train(data.InternalData);
        }
        public bool SetInputScalingParams(TrainingDataDouble data, float new_input_min, float new_input_max)
        {
            return net.set_input_scaling_params(data.InternalData, new_input_min, new_input_max);
        }
        public bool SetOutputScalingParams(TrainingDataDouble data, float new_output_min, float new_output_max)
        {
            return net.set_output_scaling_params(data.InternalData, new_output_min, new_output_max);
        }
        public bool SetScalingParams(TrainingDataDouble data, float new_input_min, float new_input_max, float new_output_min, float new_output_max)
        {
            return net.set_scaling_params(data.InternalData, new_input_min, new_input_max, new_output_min, new_output_max);
        }
        public bool ClearScalingParams()
        {
            return net.clear_scaling_params();
        }
        public void ScaleInput(double[] input_vector)
        {
            using (doubleArray inputs = new doubleArray(input_vector.Length))
            {
                for (int i = 0; i < input_vector.Length; i++)
                {
                    inputs.setitem(i, input_vector[i]);
                }
               net.scale_input(inputs.cast());
            }
        }
        public void ScaleOutput(double[] output_vector)
        {
            using (doubleArray inputs = new doubleArray(output_vector.Length))
            {
                for (int i = 0; i < output_vector.Length; i++)
                {
                    inputs.setitem(i, output_vector[i]);
                }
               net.scale_output(inputs.cast());
            }
        }
        public void DescaleInput(double[] input_vector)
        {
            using (doubleArray inputs = new doubleArray(input_vector.Length))
            {
                for (int i = 0; i < input_vector.Length; i++)
                {
                    inputs.setitem(i, input_vector[i]);
                }
               net.descale_input(inputs.cast());
            }
        }
        public void DescaleOutput(double[] output_vector)
        {
            using (doubleArray inputs = new doubleArray(output_vector.Length))
            {
                for (int i = 0; i < output_vector.Length; i++)
                {
                    inputs.setitem(i, output_vector[i]);
                }
               net.descale_output(inputs.cast());
            }
        }
        public void SetErrorLog(FannFile log_file)
        {
           net.set_error_log(log_file.InternalFile);
        }
        public uint ErrNo
        {
            get
            {
                return net.get_errno();
            }
        }
        public void ResetErrno()
        {
           net.reset_errno();
        }
        public void ResetErrstr()
        {
           net.reset_errstr();
        }
        public string ErrStr
        {
            get
            {
                return net.get_errstr();
            }
        }
        public void PrintError()
        {
           net.print_error();
        }
        public void DisableSeedRand()
        {
           net.disable_seed_rand();
        }
        public void EnableSeedRand()
        {
           net.enable_seed_rand();
        }

        public float TrainEpochBatchParallel(TrainingDataDouble data, uint threadnumb)
        {
            return fanndouble.train_epoch_batch_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        public float TrainEpochIrpropmParallel(TrainingDataDouble data, uint threadnumb)
        {
            return fanndouble.train_epoch_irpropm_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        public float TrainEpochQuickpropParallel(TrainingDataDouble data, uint threadnumb)
        {
            return fanndouble.train_epoch_quickprop_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        public float TrainEpochSarpropParallel(TrainingDataDouble data, uint threadnumb)
        {
            return fanndouble.train_epoch_sarprop_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        public float TrainEpochIncrementalMod(TrainingDataDouble data)
        {
            return fanndouble.train_epoch_incremental_mod(net.to_fann(), data.ToFannTrainData());
        }

        public float TrainEpochBatchParallel(TrainingDataDouble data, uint threadnumb, List<List<double>> predicted_outputs)
        {
            using (doubleVectorVector predicted_out = new doubleVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new doubleVector(predicted_outputs[i].Count);
                }

                float result = fanndouble.train_epoch_batch_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);

                predicted_outputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<double> list = new List<double>();
                    for(int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predicted_outputs.Add(list);
                }
                return result;
            }
        }

        public float TrainEpochIrpropmParallel(TrainingDataDouble data, uint threadnumb, List<List<double>> predicted_outputs)
        {
            using (doubleVectorVector predicted_out = new doubleVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new doubleVector(predicted_outputs[i].Count);
                }
                float result = fanndouble.train_epoch_irpropm_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);

                predicted_outputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<double> list = new List<double>();
                    for(int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predicted_outputs.Add(list);
                }
                return result;
            }
        }

        public float TrainEpochQuickpropParallel(TrainingDataDouble data, uint threadnumb, List<List<double>> predicted_outputs)
        {
            using (doubleVectorVector predicted_out = new doubleVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new doubleVector(predicted_outputs[i].Count);
                }
                float result = fanndouble.train_epoch_quickprop_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);
                
                predicted_outputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<double> list = new List<double>();
                    for(int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predicted_outputs.Add(list);
                }
                return result;
            }
        }

        public float TrainEpochSarpropParallel(TrainingDataDouble data, uint threadnumb, List<List<double>> predicted_outputs)
        {
            using (doubleVectorVector predicted_out = new doubleVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new doubleVector(predicted_outputs[i].Count);
                }
                float result = fanndouble.train_epoch_sarprop_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);
                 
                predicted_outputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<double> list = new List<double>();
                    for(int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predicted_outputs.Add(list);
                }
                return result;
            }
        }

        public float TrainEpochIncrementalMod(TrainingDataDouble data, List<List<double>> predicted_outputs)
        {
            using (doubleVectorVector predicted_out = new doubleVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new doubleVector(predicted_outputs[i].Count);
                }
                float result = fanndouble.train_epoch_incremental_mod(net.to_fann(), data.ToFannTrainData(), predicted_out);
                
                predicted_outputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<double> list = new List<double>();
                    for(int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predicted_outputs.Add(list);
                }
                return result;
            }
        }

        public float TestDataParallel(TrainingDataDouble data, uint threadnumb)
        {
            return fanndouble.test_data_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        public float TestDataParallel(TrainingDataDouble data, uint threadnumb, List<List<double>> predicted_outputs)
        {
            using (doubleVectorVector predicted_out = new doubleVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new doubleVector(predicted_outputs[i].Count);
                }
                float result = fanndouble.test_data_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);
                
                predicted_outputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<double> list = new List<double>();
                    for(int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predicted_outputs.Add(list);
                }
                return result;
            }
        }

<<<<<<< Updated upstream
=======
        /// <summary> Callback, called when the set. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="callback"> The callback. </param>
        /// <param name="userData"> Information describing the user. </param>

        public void SetCallback(TrainingCallbackDouble callback, Object userData)
        {
            Callback = callback;
            UserData = userData;
            GCHandle handle = GCHandle.Alloc(userData);
            training_callback back = new training_callback(InternalCallback);
            fanndoublePINVOKE.neural_net_set_callback(neural_net.getCPtr(this.net), Marshal.GetFunctionPointerForDelegate(back), (IntPtr)handle);
        }

        private int InternalCallback(global::System.IntPtr netPtr, global::System.IntPtr dataPtr, uint max_epochs, uint epochs_between_reports, float desired_error, uint epochs, global::System.IntPtr user_data)
        {
            NeuralNetDouble callbackNet = new NeuralNetDouble(new neural_net(netPtr, false));
            TrainingDataDouble callbackData = new TrainingDataDouble(new training_data(dataPtr, false));
            GCHandle handle = (GCHandle)user_data;
            return Callback(callbackNet, callbackData, max_epochs, epochs_between_reports, desired_error, epochs, handle.Target as Object);
        }


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate int training_callback(global::System.IntPtr net, global::System.IntPtr data, uint max_epochs, uint epochs_between_reports, float desired_error, uint epochs, global::System.IntPtr user_data);

>>>>>>> Stashed changes
#region Properties
        public neural_net InternalFloatNet
        {
            get
            {
                return net;
            }
        }

        private uint Outputs { get; set; }
#endregion Properties
    }
}
